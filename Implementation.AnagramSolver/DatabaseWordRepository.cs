﻿using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class DatabaseWordRepository : IWordRepository
    {
        private Dictionary<string, int> _dictionary;

        public DatabaseWordRepository(string fileName)
        {
            Dictionary<string, int> wordsList = new Dictionary<string, int>();

            string sql = @"INSERT INTO dbo.Words (Word) VALUES (test)";

            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnagramsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {

                using (SqlCommand command = new SqlCommand("select * from Words", conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!wordsList.ContainsKey(reader["Word"].ToString()))
                            {
                                wordsList.Add(reader["Word"].ToString(), 1);
                            }
                        }
                    }
                }
            }

            _dictionary = wordsList;
        }

        public List<string> GetWords(int skip, int take)
        {
            return _dictionary.Keys.ToList().Skip(skip * take).Take(take).ToList();
        }

        public void AddWord(string input)
        {

            var inputWords = input.Split();

            KeyValuePair<string, int> keyValue;

            if (!_dictionary.ContainsKey(inputWords[0]))
            {
                _dictionary.Add(inputWords[0], Int32.Parse(inputWords[inputWords.Length - 1]));
            }
            if (!_dictionary.ContainsKey(inputWords[inputWords.Length - 2]))
            {
                _dictionary.Add(inputWords[inputWords.Length - 2], Int32.Parse(inputWords[inputWords.Length - 1]));
            }

            //((ICollection<KeyValuePair<string, int>>)_dictionary).Add(keyValue);
        }

        public Dictionary<string, int> GetWordsDictionary()
        {
            return _dictionary;
        }
    }
}