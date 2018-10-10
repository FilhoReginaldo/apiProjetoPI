using System;
using System.Collections.Generic;
using Efficacy.Api.Models.Request;
using Efficacy.Api.Models.Response;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Efficacy.Api.DataAcess.Entities;

namespace Efficacy.Api.Business
{
    public class FamiliaBusiness: IDisposable
    {
        private DataAcess.Entities.ProjetoAPIContext data = null;

        public FamiliaBusiness(DbContextOptions<ProjetoAPIContext> options)
        {
            data = new DataAcess.Entities.ProjetoAPIContext(options);
        }


        public ListarFamiliaResponse FamiliaBuscar(ListaFamiliaRequest request)
        {
            ListarFamiliaResponse response = new ListarFamiliaResponse();

            try
            {
                var resultado = from familia in data.FAMILIA
                                select new FamiliaResponse()
                                {
                                    ID = familia.ID,
                                    Nome = familia.Nome,
                                    Descricao = familia.Descricao
                                };

                if (request != null)
                {
                    if(request.ID.HasValue)
                    {
                        resultado = resultado.Where(whr => whr.ID == request.ID);
                    }

                    if(!string.IsNullOrEmpty(request.Nome))
                    {
                        resultado = resultado.Where(whr => request.Nome.Contains(whr.Nome));
                    }
                }
                else
                {
                    throw new Exception("Obejto não preenchido corretamente!");
                }
                
                response.Familia = resultado.ToList();
                response.Sucesso = true;
                response.Mensagem = "Processado com sucesso!";

            }
            catch (Exception err)
            {
                response.Sucesso = false;
                response.Mensagem = err.Message;

            }

            return response;
        }

        public GravarFamiliaResponse GravarFamilia(GravarFamiliaRequest request)
        {
            GravarFamiliaResponse response = new GravarFamiliaResponse();

            try
            {
                if (request == null) throw new Exception("O objeto request não foi preenchido.");

                FAMILIA familia = data.FAMILIA.Where(whr => whr.ID == request.ID).FirstOrDefault();

                if(familia == null)
                {
                    familia = new FAMILIA()
                    {
                        Nome = request.Nome,
                        Descricao = request.Descricao
                    };

                    data.Add(familia);
                    data.SaveChanges();
                }
                else
                {
                    familia.Nome = request.Nome;
                    familia.Descricao = request.Descricao;

                    data.Update(familia);
                    data.SaveChanges();
                }

                response.ID = familia.ID;
                response.Sucesso = true;
                response.Mensagem = "Registro Criado com Sucesso!";

            }
            catch(Exception err)
            {
                response.ID = 0;
                response.Sucesso = false;
                response.Mensagem = err.Message;
            }

            return response;
        }

        public BaseResponse ExcluirFamilia (int ID)
        {

            BaseResponse response = new BaseResponse();

            try
            {
                var familia = data.FAMILIA.Where(whr => whr.ID == ID).FirstOrDefault();

                if(familia == null)
                {
                    throw new Exception ("A Familia informada não foi encontrada.");

                }

                data.Remove(familia);
                data.SaveChanges();

                response.Sucesso = true;
                response.Mensagem = "Registro Excluido com Sucesso!";

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