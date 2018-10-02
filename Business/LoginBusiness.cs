using System;
using System.Collections.Generic;
using Efficacy.Api.Models.Request;
using Efficacy.Api.Models.Response;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Efficacy.Api.DataAccess;

namespace Efficacy.Api.Business
{
    public class LoginBusiness: IDisposable
    {
        private DataAccess.ProjetoAPIContext data = null;

        public LoginBusiness(DbContextOptions<ProjetoAPIContext> options)
        {
            data = new DataAccess.ProjetoAPIContext(options);
        }

        public LoginResponse ValidarLogin(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();

            try
            {
                if(request == null)throw new Exception("O objeto request não foi preenchido.");

                PESSOA pessoa = data.PESSOA.Where(whr => whr.Email == request.Email).FirstOrDefault();

                if(pessoa.Email != request.Email || pessoa.Senha != request.Senha)
                {
                    response.Sucesso =false;
                    response.Mensagem = "Usuário ou senha invalido!";

                }
                else if(pessoa.Email == request.Email && pessoa.Senha == request.Senha)
                {
                    response.PessoaID = pessoa.ID;
                    response.Sucesso = true;
                    response.Mensagem = "Login Efetuado com Sucesso!";
                }
            }
            catch(Exception err)
            {
                response.Sucesso =false;
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