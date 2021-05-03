using Dapper;
using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ORM.Interfaces
{
    public class TodoRepository : RepositoryConnector, IToDoRepository
    {
        public TodoRepository(IConfiguration config) :base(config) { }

        public void Add(ToDo obj)
        {
            DynamicParameters pam = new DynamicParameters();
            pam.Add("@Tarefa", obj.Tarefa);

            string sql = "INSERT INTO Todo (Tarefa) VALUES(@Tarefa)";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, pam);
            }
        }

        public ToDo Get(int id)
        {
            string sql = $"SELECT * FROM Todo WHERE id = {id}";
            using (var con = new SqlConnection(base.GetConnection()))
            {
                return con.Query<ToDo>(sql).FirstOrDefault();
            }

        }

        public IEnumerable<ToDo> GetAll()
        {
            IEnumerable<ToDo> retorno;
            string sql = "SELECT * FROM Todo";
            using (var con = new SqlConnection(base.GetConnection()))
            {
                retorno = con.Query<ToDo>(sql);
            }
            return retorno;
        }

        public void Remove(ToDo obj)
        {
            string sql = $"DELETE FROM Todo WHERE id = {obj.Id}";
            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql);
            }
        }

        public void Update(ToDo obj)
        {
            string sql = $@"UPDATE Todo
                            SET Tarefa = @Tarefa
                            WHERE Id = {obj.Id}";

            DynamicParameters pam = new DynamicParameters();
            pam.Add("@Tarefa", obj.Tarefa);
            using(var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, pam);
            }
        }
    }
}
