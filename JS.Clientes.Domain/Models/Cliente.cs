﻿
using JS.Core.DomainObjects;

namespace JS.Clientes.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public string Nome { get; set; }
        public Email Email { get; set; }
        public Cpf Cpf { get; set; }
        public string Telefone { get; set; }
        public bool Excluido { get; set; }
        public bool EhFornecedor { get; set; }
        public bool EhCliente { get; set; }

        //EF Relation
        public Endereco Endereco { get; set; }

        // EF Relation
        public Cliente() { }        

        public void TrocarEmail(string email)
        {
            Email = new Email(email);
        }

        public void AtribuirEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }
    }
}