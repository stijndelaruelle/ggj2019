using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Sjabloon
{
    public static class UtilityMethods
    {
        public static bool IsParent(Transform target, Transform potentionalParent)
        {
            Transform currentTransform = target;

            while (currentTransform != null)
            {
                if (currentTransform == potentionalParent)
                    return true;

                currentTransform = currentTransform.parent;
            }

            return false;
        }

        public static string RemoveExtention(string source)
        {
            int lastDotID = source.LastIndexOf(".");
            return source.Substring(0, lastDotID);
        }

        //CSV parsing
        public static string ROW_SEPARATOR = ";|;";
        public static string COLUMN_SEPARATOR = ";-;";

        public static string[,] ParseCSV(string filename)
        {
            string fileText = "";
            try
            {
                fileText = File.ReadAllText(filename, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                //The file was not found, but that shouldn't crash the game!
                Debug.LogError(e.Message);
                return null;
            }

            return ParseCSVRaw(fileText);
        }

        public static string[,] ParseCSVRaw(string fileText)
        {
            string[,] result = new string[0, 0];

            //Split the text in rows
            string[] srcRows = fileText.Split(new string[] { ROW_SEPARATOR }, StringSplitOptions.None); //new char[] { '\r', '\n' }
            List<string> rows = new List<string>(srcRows);
            rows.RemoveAll(rowName => rowName == "");

            //Split the rows in colmuns
            for (int y = 0; y < rows.Count; ++y)
            {
                string[] srcColumns = rows[y].Split(new string[] { COLUMN_SEPARATOR }, StringSplitOptions.None); //new char[] { ';' }

                //Create new 2 dimensional array if required (we only now know the size)
                if (result.Length == 0)
                    result = new string[srcColumns.Length, rows.Count];

                for (int x = 0; x < srcColumns.Length; ++x)
                {
                    //Pretty much impossible, just an extra safety net
                    if (x >= result.GetLength(0))
                    {
                        Debug.LogWarning("Row consists of more columns than the first row of the table! Source: " + rows[y]);
                        return null;
                    }

                    result[x, y] = srcColumns[x];
                }
            }

            return result;
        }
    }
}