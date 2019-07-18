using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class DatabaseWordRepository : IWordRepository
    {
        private List<string> _dictionary;

        public DatabaseWordRepository()
        {
            List<string> wordsList = new List<string>();

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
                            if (!wordsList.Contains(reader["Word"].ToString()))
                            {
                                wordsList.Add(reader["Word"].ToString());
                            }
                        }
                    }
                }
            }

            _dictionary = wordsList;
        }

        public List<string> GetWords(int skip, int take)
        {
            return _dictionary.OrderBy(x => x).Skip(skip * take).Take(take).ToList();
        }

        public void AddWord(string input)
        {

            var inputWords = input.Split();

            KeyValuePair<string, int> keyValue;

            if (!_dictionary.Contains(inputWords[0]))
            {
                _dictionary.Add(inputWords[0]);
            }
            if (!_dictionary.Contains(inputWords[inputWords.Length - 2]))
            {
                _dictionary.Add(inputWords[inputWords.Length - 2]);
            }
        }

        public List<string> GetWordsDictionary()
        {
            return _dictionary;
        }

        public List<string> GetWordsContainingPart(string searchPhrase)
        {
            throw new System.NotImplementedException();
        }

        public bool EditWord(string originalWord, string newWord)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteWord(string wordToDelete)
        {
            throw new System.NotImplementedException();
        }

        void IWordRepository.DeleteWord(string wordToDelete)
        {
            throw new System.NotImplementedException();
        }
    }
}
