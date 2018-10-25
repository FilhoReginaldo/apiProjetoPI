using System;
using System.Collections.Generic;
using System.Linq;
using Efficacy.Api.DataAcess.Entities;
using Efficacy.Api.Models.Request;
using Efficacy.Api.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Efficacy.Api.Business
{
    public class TipoBussiness: IDisposable
    {
        private DataAcess.Entities.ProjetoAPIContext data = null;

        public TipoBussiness(DbContextOptions<ProjetoAPIContext> options)
        {
            data = new DataAcess.Entities.ProjetoAPIContext(options);
        }

        public ListarTipoResponse ListarTipo(ListarTipoRequest request)
        {
            ListarTipoResponse response = new ListarTipoResponse();

            try
            {
                var resultado = from tipo in data.TIPO
                            select new TipoResponse()
                            {
                                ID = tipo.ID,
                                FamiliaID = tipo.FamiliaID,
                                Nome = tipo.Nome,
                                Descricao = tipo.Descricao,
                                Origem = tipo.Origem,

                                Familia =(from familia in data.FAMILIA
                                         where familia.ID == tipo.FamiliaID
                                         select new FamiliaResponse()
                                         {
                                             ID = familia.ID,
                                             Nome = familia.Nome,
                                             Descricao = familia.Descricao
                                         }).ToList()
                            };
                
                if(request != null)
                {
                    if(request.ID.HasValue)
                    {
                        resultado = resultado.Where(whr => whr.ID == request.ID);
                    }
                    if(!string.IsNullOrEmpty(request.Nome))
                    {
                        resultado = resultado.Where(whr => whr.Nome.Contains(request.Nome));
                    }
                }
                else
                {
                    throw new Exception("O Objeto não foi enviado corretamente!");
                }

                response.Tipo = resultado.ToList();
                response.Sucesso = true;
                response.Mensagem = "Processado com Sucesso";

            }
            catch (System.Exception err)
            {
                response.Sucesso = false;
                response.Mensagem = err.Message;
            }
            
            return response;
        }

        public GravarTipoResponse GravarTipo (GravarTipoRequest request)
        {
            var response = new GravarTipoResponse();

            try
            {
                if(request == null) throw new Exception("O Objeto não foi preenchido!");

                TIPO tipo = data.TIPO.Where(whr => whr.ID == request.ID).FirstOrDefault();

                FAMILIA familia = data.FAMILIA.Where(whr => whr.ID == request.FamiliaID).FirstOrDefault();

                if(familia == null)
                {
                    throw new Exception("FamiliaID não foi encontrada.");
                }     
                else
                {
                    if(tipo == null)
                    {
                        tipo = new TIPO
                        {
                            FamiliaID = request.FamiliaID,
                            Descricao = request.Descricao,
                            Nome = request.Nome,
                            Origem = request.Origem
                        };

                        data.Add(tipo);
                        data.SaveChanges();
                    }
                    else
                    {
                        tipo.FamiliaID = request.FamiliaID;
                        tipo.Descricao = request.Descricao;
                        tipo.Nome = request.Nome;
                        tipo.Origem = request.Origem;

                        data.Update(tipo);
                        data.SaveChanges();
                    }
                }
                
                response.ID = tipo.ID;
                response.Sucesso = true;
                response.Mensagem = "Registro criado com Sucesso!";
                
            }
            catch (Exception err)
            {
                response.ID = 0;
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