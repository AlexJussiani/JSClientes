using FluentValidation.Results;
using JS.Clientes.Domain.Models;
using JS.Clientes.Infra.Repository;
using JS.Core.Messages;
using JS.WebAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JS.Clientes.API.Services
{   
    public class ClienteServices : CommandHandler
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteServices(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> AtualizarCliente(Guid id, Cliente cliente)
        {
           
            var cpf = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);
            var email = await _clienteRepository.ObterPorEmail(cliente.Email.Endereco);
            if(email != null && email.Id != id)
            {
                AdicionarErro("Este email já está em uso.");
                return ValidationResult;
            }
            if (cpf != null && cpf.Id != id)
            {
                AdicionarErro("Este CPF já está em uso.");
                return ValidationResult;
            }
            var c = await _clienteRepository.ObterClientePorId(id);
            cliente.Id = c.Id;
            _clienteRepository.AtualizarCliente(cliente);
            return await PersistirDados(_clienteRepository.UnitOfWork);
        }

        public async Task<ValidationResult> AdicionarEndereco(Endereco endereco)
        {
            var idCliente = await _clienteRepository.ObterClientePorId(endereco.ClienteId);
            if (idCliente == null)
            {
                AdicionarErro("Esse endereço não está vinculado a nenhum cliente");
                return ValidationResult;
            }
            _clienteRepository.AdicionarEndereco(endereco);
            return await PersistirDados(_clienteRepository.UnitOfWork);
        }

        public async Task<ValidationResult> AtualizarEndereco(Guid id, Endereco endereco)
        {
            if(id != endereco.Id)
            {
                AdicionarErro("O id informado não é o mesmo que foi passado na query");
                return ValidationResult;
            }
            _clienteRepository.AtualizarEndereco(endereco);
            return await PersistirDados(_clienteRepository.UnitOfWork);
        }

        public async Task<ValidationResult> CadastrarCliente(Cliente cliente)
        {
            if (!string.IsNullOrEmpty(cliente.Cpf.Numero))
            {
                var cpf = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);
                if (cpf != null)
                {
                    AdicionarErro("Este CPF já está em uso.");
                    return ValidationResult;
                }
            }

            if (!string.IsNullOrEmpty(cliente.Email.Endereco))
            {
                var email = await _clienteRepository.ObterPorEmail(cliente.Email.Endereco);
                if (email != null)
                {
                    AdicionarErro("Este Email já está em uso.");
                    return ValidationResult;
                }
            }
            _clienteRepository.AdicionarCliente(cliente);

            return await PersistirDados(_clienteRepository.UnitOfWork);            
        }

        public async Task<PagedResult<Cliente>> ObterClientesPaginacao(int pageSize, int pageIndex, string query = null)
        {
            var clientes = await _clienteRepository.ObterPageTodosPaginacao(pageSize,pageIndex,query);  
            
            return new PagedResult<Cliente>()
            {
                List = clientes,
                TotalResults = await _clienteRepository.TotalClientes(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }

        public async Task<IEnumerable<Cliente>> ObterClientes()
        {
            var clientes = await _clienteRepository.ObterPageTodos();

            return clientes;
        }

        public async Task<IEnumerable<Cliente>> ObterListaFornecedores()
        {
            var clientes = await _clienteRepository.ObterListaFornecedores();

            return clientes;
        }

        public async Task<IEnumerable<Cliente>> ObterListaClientes()
        {
            var clientes = await _clienteRepository.ObterListaClientes();

            return clientes;
        }

        public async Task<ValidationResult> DeletarCliente(Guid id)
        {
            var cliente = await _clienteRepository.ObterClientePorId(id);
            _clienteRepository.DeletarCliente(cliente);
            return await PersistirDados(_clienteRepository.UnitOfWork);
        }

        public async Task<Cliente> ObterPorIdService(Guid id)
        {
            return await _clienteRepository.ObterClientePorId(id);            
        }     

        public async Task<Endereco> ObterEnderecoPorId(Guid id)
        {
            return await _clienteRepository.ObterEnderecoPorId(id);
        }

        public async Task<Cliente> ObterPorCpfService(string cpf)
        {
            return await _clienteRepository.ObterPorCpf(cpf);
        }
    }
}
