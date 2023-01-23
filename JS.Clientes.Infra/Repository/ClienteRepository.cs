using JS.Clientes.Domain.Models;
using JS.Clientes.Infra.Data;
using JS.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JS.Clientes.Infra.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClientesContext _context;

        public ClienteRepository(ClientesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Cliente>> ObterPageTodosPaginacao(int pageSize, int pageIndex, string query = null)
        {
            return await _context.Clientes.AsNoTracking().Where(c => c.Excluido == false).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterPageTodos()
        {
            return await _context.Clientes.AsNoTracking().Where(c => c.Excluido == false).ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterListaFornecedores()
        {
            return await _context.Clientes.AsNoTracking().Where(c => c.Excluido == false && c.EhFornecedor == true).ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterListaClientes()
        {
            return await _context.Clientes.AsNoTracking().Where(c => c.Excluido == false && c.EhCliente == true).ToListAsync();
        }

        public async Task<int> TotalClientes()
        {
            return await _context.Clientes.AsNoTracking()  
                .Where(c => c.Excluido == false).CountAsync();
        }

        public Task<Cliente> ObterPorCpf(string cpf)
        {  
            return _context.Clientes
                .Include(c => c.Endereco)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        }
        public Task<Endereco> ObterEndereco(Guid id)
        {
            return _context.Enderecos
                // .Include(c => c.Endereco)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }


        public Task<Cliente> ObterPorEmail(string email)
        {
            return _context.Clientes.FirstOrDefaultAsync(c => c.Email.Endereco == email && c.Excluido == false);
        }      

        public async Task<Cliente> ObterClientePorId(Guid id)
        {
            return await _context.Clientes
                .Include(c => c.Endereco)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void AdicionarCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
        }

        public async Task<Endereco> ObterEnderecoPorId(Guid id)
        {
            return await _context.Enderecos.FirstOrDefaultAsync(e => e.ClienteId == id);
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void AtualizarCliente(Cliente cliente)
        {
            _context.Update(cliente);
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
        }

        public void DeletarCliente(Cliente cliente)
        {
            _context.Remove(cliente);
        }       
    }
}