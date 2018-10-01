using System;
using System.Collections.Generic;
using Efficacy.Api.Models.Request;
using Efficacy.Api.Models.Response;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Efficacy.Api.DataAccess.Entities;

namespace Efficacy.Api.Business
{
    public class PessoaBusiness: IDisposable
    {
        private DataAccess.Entities.APIContext data = null;

        public PessoaBusiness(DbContextOptions<APIContext> options)
        {
            data = new DataAccess.Entities.APIContext(options);
        }

        public ListarPessoasResponse ListarPessoas(ListarPessoasRequest request)
        {
            ListarPessoasResponse response = new ListarPessoasResponse();

            try
            {
                
                var resultado = from pess in data.PESSOA
                                select new PessoaResponse()
                                {
                                    Nome = pess.Nome,
                                    ID = pess.ID,
                                    DataCriacao = pess.DataCriacao,
                                    DataNascimento = pess.DataNascimento,
                                    
                                    Enderecos = (from end in data.PESSOA_ENDERECO
                                                 where end.PessoaID == pess.ID
                                                 select new PessoaEnderecoResponse()
                                                 {
                                                    ID = end.ID,
                                                    PessoaID = end.PessoaID,
                                                    Logradouro = end.Logradouro,
                                                    Bairro = end.Bairro,
                                                    CEP = end.CEP,
                                                    Cidade = end.Cidade,
                                                    UF = end.UF       
                                                
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
                        Nome = request.Nome,
                        DataNascimento = request.DataNascimento,
                        DataCriacao = DateTime.Now
                    };

                    data.Add(pessoa);
                    data.SaveChanges();
        
                } 
                else
                {
                    pessoa.Nome = request.Nome;
                    pessoa.DataNascimento = request.DataNascimento;

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
                                Logradouro = item.Logradouro,
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