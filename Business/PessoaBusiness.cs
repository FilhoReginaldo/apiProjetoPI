using System;
using System.Collections.Generic;
using Efficacy.Api.Models.Request;
using Efficacy.Api.Models.Response;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Efficacy.Api.DataAccess;

namespace Efficacy.Api.Business
{
    public class PessoaBusiness: IDisposable
    {
        private DataAccess.ProjetoAPIContext data = null;

        public PessoaBusiness(DbContextOptions<ProjetoAPIContext> options)
        {
            data = new DataAccess.ProjetoAPIContext(options);
        }

        public ListarPessoasResponse ListarPessoas(ListarPessoasRequest request)
        {
            ListarPessoasResponse response = new ListarPessoasResponse();

            try
            {
                
                var resultado = from pess in data.PESSOA
                                select new PessoaResponse()
                                {
                                    ID = pess.ID,
                                    TipoPessoaID = pess.TipoPessoaID,
                                    RazaoSocial = pess.RazaoSocial,
                                    Nome = pess.Nome,
                                    Email = pess.Email,
                                    Senha = pess.Senha,
                                    Cpf_Cnpj = pess.Cpf_Cnpj,
                                    Telefone = pess.Telefone,
                                    Celular = pess.Celular,
                                    DataNascimento = pess.DataNascimento,
                                    DataCriacao = pess.DataCriacao,
                                    
                                    Enderecos = (from end in data.PESSOA_ENDERECO
                                                 where end.PessoaID == pess.ID
                                                 select new PessoaEnderecoResponse()
                                                 {
                                                    ID = end.ID,
                                                    PessoaID = end.PessoaID,
                                                    EnderecoCobranca = end.EnderecoCobranca,
                                                    CEP = end.CEP,
                                                    UF = end.UF,
                                                    Cidade = end.Cidade,
                                                    Bairro = end.Bairro,   
                                                    Logradouro = end.Logradouro,
                                                    Numero = end.Numero
                                                 }).ToList()        
                               
                                };

                
                if (request != null)
                {
                    if (request.ID.HasValue)
                    {
                        resultado = resultado.Where( whr => whr.ID == request.ID );
                    }

                    if( string.IsNullOrEmpty(request.Nome) == false ) 
                    {
                        resultado = resultado.Where( whr => request.Nome.Contains(whr.Nome));
                    }

                    if( string.IsNullOrEmpty(request.Email)==false)
                    {
                        resultado = resultado.Where(whr => whr.Email == request.Email);
                    }

                }


                response.Pessoas = resultado.ToList();
                response.Sucesso = true;
                response.Mensagem = "O registro foi processado com sucesso.";
            
            }
            catch (System.Exception err)
            {
                response.Sucesso = false;
                response.Mensagem = err.Message;
            }

            return response;
        }


        public GravarPessoaResponse GravarPessoa(GravarPessoaRequest request)
        {
            GravarPessoaResponse response = new GravarPessoaResponse();

            var transaction = data.Database.BeginTransaction();

            try
            {
                if (request == null) throw new Exception("O objeto request não foi preenchido.");

                PESSOA pessoa = data.PESSOA.Where(whr => whr.ID == request.ID).FirstOrDefault();

                if (pessoa == null)
                {
                    pessoa = new PESSOA()
                    {
                        TipoPessoaID = request.TipoPessoaID,
                        RazaoSocial = request.RazaoSocial,
                        Nome = request.Nome,
                        Email = request.Email,
                        Senha = request.Senha,
                        Cpf_Cnpj = request.Cpf_Cnpj,
                        Telefone = request.Telefone,
                        Celular = request.Celular,
                        DataNascimento = request.DataNascimento,
                        DataCriacao = DateTime.Now,
                        DataAlteracao = DateTime.Now
                    };

                    data.Add(pessoa);
                    data.SaveChanges();
        
                } 
                else
                {
                    pessoa.Nome = request.Nome;
                    pessoa.DataNascimento = request.DataNascimento;
                    pessoa.Cpf_Cnpj = request.Cpf_Cnpj;
                    pessoa.Email = request.Email;
                    pessoa.Senha = request.Senha;
                    pessoa.Telefone = request.Telefone;
                    pessoa.Celular = request.Celular;
                    pessoa.Senha = request.Senha;
                    pessoa.DataNascimento = request.DataNascimento;
                    pessoa.DataAlteracao = DateTime.Now;

                    data.Update(pessoa);
                    data.SaveChanges();

                }

                if (request.Enderecos != null)
                {
                    foreach(var item in request.Enderecos)
                    {
                        PESSOA_ENDERECO endereco = data.PESSOA_ENDERECO.Where(whr => whr.PessoaID == pessoa.ID).FirstOrDefault();    

                        if (endereco == null)
                        {
                            endereco = new PESSOA_ENDERECO()
                            {
                                PessoaID = pessoa.ID,
                                EnderecoCobranca = item.EnderecoCobranca,
                                Logradouro = item.Logradouro,
                                Numero = item.Numero,
                                Bairro = item.Bairro,
                                CEP = item.CEP,
                                Cidade = item.Cidade,
                                UF = item.UF
                                
                            }; 

                            data.Add(endereco);
                            data.SaveChanges();
                        }
                        else
                        {
                            endereco.Logradouro = item.Logradouro;
                            endereco.EnderecoCobranca = item.EnderecoCobranca;
                            endereco.Numero = item.Numero;
                            endereco.CEP = item.CEP;
                            endereco.Bairro = item.Bairro;
                            endereco.Cidade = item.Cidade;
                            endereco.UF = item.UF;    


                            data.Update(endereco);
                            data.SaveChanges();
       
                        }
       
                    }
                }

                response.ID = pessoa.ID;
                response.Sucesso = true;
                response.Mensagem = "O Registro foi salvo com sucesso.";

                transaction.Commit();
                
            }
            catch (System.Exception err)
            {
                transaction.Rollback();
                response.Sucesso = false;
                response.Mensagem = err.Message;
            }

            return response;
        }

        public BaseResponse ExcluirPessoa(int ID)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                var pessoa = data.PESSOA.Where(whr => whr.ID == ID).FirstOrDefault();

                if (pessoa == null)
                    throw new Exception("A Pessoa informada não foi encontrada.");

                data.Remove(pessoa);
                data.SaveChanges();    

                response.Sucesso = true;
                response.Mensagem = "O registro foi excluido com sucesso.";       
            }
            catch(Exception err)
            {
                response.Sucesso = false;
                response.Mensagem = err.Message;
            }

            return response;
        }

        public BaseResponse ExcluirEndereco(int ID){
            BaseResponse response = new BaseResponse();

            try{
                var pessoaEndereco = data.PESSOA_ENDERECO.Where(whr => whr.ID ==ID).FirstOrDefault();

                if(pessoaEndereco == null) throw new Exception("O Endereço não foi encontrado!");

                data.Remove(pessoaEndereco);
                data.SaveChanges();

                response.Sucesso = true;
                response.Mensagem = "O Registro foi excluido com sucesso!";

            }
            catch(Exception err)
            {
                response.Sucesso = false;
                response.Mensagem = err.Message;
            }

            return response;
        }       

        public void Dispose()
        {
            data.Dispose();
        }

    }

}