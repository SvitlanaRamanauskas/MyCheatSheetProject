﻿using System;
using System.Data.SQLite;

namespace CheatSheetProject.Repositories
{
    public class SQLTableManagement
    {
        private static SQLiteConnection conn;
        public SQLTableManagement()
        {
        }

        public static void ExecuteSQL(string sqlStatement)
        {
            SQLiteCommand sqlite_cmd = GetSQLiteConnection().CreateCommand();
            sqlite_cmd.CommandText = sqlStatement;
            sqlite_cmd.ExecuteNonQuery();
        }

        public static void InsertData(string tableName, string columnNames, string values)
        {
            SQLiteCommand sqlite_cmd = GetSQLiteConnection().CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO {tableName} ({columnNames}) VALUES({values}); ";
            sqlite_cmd.ExecuteNonQuery();
        }

        public static SQLiteDataReader ReadData(string tableName, string? clause)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = GetSQLiteConnection().CreateCommand();
            if (clause == null)
            {
                sqlite_cmd.CommandText = $"SELECT * FROM {tableName}";
            }
            else
            {
                sqlite_cmd.CommandText = $"SELECT * FROM {tableName} WHERE {clause}";
            }
            return sqlite_cmd.ExecuteReader();
        }

        public static SQLiteDataReader ReadCustomData(string customSelectStatement)
        {
            SQLiteCommand sQLiteCommand = GetSQLiteConnection().CreateCommand();
            sQLiteCommand.CommandText = customSelectStatement;
            return sQLiteCommand.ExecuteReader();
        }


        public static void UpdateData(string tableName, string setValues, string clause)
        {
            SQLiteCommand sqlite_cmd = GetSQLiteConnection().CreateCommand();
            sqlite_cmd.CommandText = $"UPDATE {tableName} SET {setValues} WHERE {clause}; ";
            sqlite_cmd.ExecuteNonQuery();
        }

        public static void DeleteData(string tableName, string clause)
        {
            SQLiteCommand sqlite_cmd = GetSQLiteConnection().CreateCommand();
            sqlite_cmd.CommandText = $"DELETE from {tableName} WHERE {clause}; ";
            sqlite_cmd.ExecuteNonQuery();
        }

        public static void CloseConnections(SQLiteDataReader sqlite_datareader)
        {
            sqlite_datareader.Close();
            SQLTableManagement.GetSQLiteConnection().Close();
        }

        public static SQLiteConnection GetSQLiteConnection()
        {
            if (conn == null)
            {
                conn = SqliteConnect.CreateConnection();
            }
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

    }
}
