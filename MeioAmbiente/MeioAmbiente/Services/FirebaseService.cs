using Firebase.Database;
using Firebase.Database.Query;
using MeioAmbiente.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioAmbiente.Services
{
    public class FirebaseService
    {
        FirebaseClient client;

        public FirebaseService()
        {
            client = new FirebaseClient("https://meioambientexamarin.firebaseio.com/");
        }
        public async Task<bool> AdicionarCatastrofe(Registrar registrar)
        {
            await client.Child("Registros")
                .PostAsync(new Registrar()
                {
                    DeslizamentoTerra = registrar.DeslizamentoTerra,
                    Queimada = registrar.Queimada,
                    RompimentoBarragem = registrar.RompimentoBarragem,
                    DataRegistro = registrar.DataRegistro,
                    Alagamento = registrar.Alagamento,
                    Outros = registrar.Outros,
                    AreaTexto = registrar.AreaTexto,
                    Pesquisa = registrar.Pesquisa
                });
            return true;
        }
        public async Task <List<Registrar>> GetList()
        {
            var list = (await client
                .Child("Registros")
                .OnceAsync<Registrar>())
                .Select(item =>
                        new Registrar
                        {
                            Alagamento = item.Object.Alagamento,
                            Queimada = item.Object.Queimada,
                            DeslizamentoTerra = item.Object.DeslizamentoTerra,
                            RompimentoBarragem = item.Object.RompimentoBarragem,
                            Outros = item.Object.Outros,
                            DataRegistro = item.Object.DataRegistro,
                            AreaTexto = item.Object.AreaTexto,
                            Pesquisa = item.Object.Pesquisa
                        }).ToList();
            return list;
        }
        public async Task<bool> LoginUser(string name, string password)
        {
            var user = (await client.Child("Users")
                .OnceAsync<User>())
                .Where(u => u.Object.Username == name)
                .Where(u => u.Object.Password == password)
                .FirstOrDefault();

            return (user != null);
        }
        public async Task<bool> RegistrarUser(string name, string password,string telefone, string email)
        {
            if(await LoginUser(name,password)==false)
            {
                await client.Child("Users")
                    .PostAsync(new RegistrarUsuario()
                    {
                        Username = name,
                        Password = password,
                        Email = email,
                        Telefone = telefone
                    });
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
