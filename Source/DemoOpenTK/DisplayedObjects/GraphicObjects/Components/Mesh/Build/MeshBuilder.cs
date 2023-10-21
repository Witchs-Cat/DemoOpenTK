using Microsoft.Extensions.Logging;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace DemoOpenTK
{
    public class MeshBuilder
    {
        private readonly LinkedList<Vector3> _coordinates;
        private readonly LinkedList<Vector3> _normals;
        private readonly LinkedList<Vector3> _textures;
        private readonly LinkedList<Polygon> _faces;

        private static readonly CultureInfo _culture = new CultureInfo("Ru-ru", true);
        private static readonly NumberFormatInfo _numberFormat = _culture.NumberFormat;

        static MeshBuilder()
        {
            _numberFormat.NumberDecimalSeparator = ".";
        }

        public MeshBuilder(ILogger<MeshBuilder>? logger = null)    
        {
            _coordinates = new LinkedList<Vector3>();
            _normals = new LinkedList<Vector3>();
            _textures = new LinkedList<Vector3>(); 
            _faces = new LinkedList<Polygon>();

        }

        public MeshBuilder LoadFromFile(string pathToFile)
        {
            IEnumerable<string> lines = File.ReadLines(pathToFile);
            
            foreach (string line in lines)
            {
                if (line.Length < 2)
                    continue;

                switch (line[..2])
                {
                    case "# ":
                        continue;
                    case "v ":
                        _coordinates.AddLast(ParseVector3(line[3..]));
                        break;
                    case "vn":
                        _normals.AddLast(ParseVector3(line[3..]));
                        break;
                    case "vt":
                        _textures.AddLast(ParseVector3(line[3..]));
                        break;
                    case "f ":
                        _faces.AddLast(ParsePolygon(line[2..]));
                        break;
                }

            }

            return this;
        }

        public Mesh Build()
        {
            int size = _faces.Count * 3;
            Vector3[] coordinates = new Vector3[size];
            Vector3[] normals = new Vector3[size];
            Vector2[] textures = new Vector2[size];

            uint index = 0;
            foreach (Polygon polygon in _faces)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector3i currentVertex = polygon[i];

                    coordinates[index] =  _coordinates.ElementAt(currentVertex[0] - 1);
                    textures[index] = _textures.ElementAt(currentVertex[1] - 1).Xy;
                    normals[index] = _normals.ElementAt(currentVertex[2] - 1);
                    index++;
                } 

            }

            return new Mesh(coordinates.ToArray(), normals.ToArray(), textures.ToArray());
        }


        private Polygon ParsePolygon(string str)
        {
            IEnumerable<Vector3i> polygon = str.Split(" ").Select(x => ParseVector3i(x, "/"));
            return new Polygon(polygon.ElementAt(0), polygon.ElementAt(1), polygon.ElementAt(2));
        }

        private static Vector3 ParseVector3(string str, string seporator = " ")
        {
            IEnumerable<float> vector = str.Split(seporator).Select(x => Single.Parse(x, _numberFormat));
            return new Vector3(vector.ElementAt(0), vector.ElementAt(1), vector.ElementAt(2));
        }

        private static Vector3i ParseVector3i(string str, string seporator = " ")
        {
            IEnumerable<int> vector = str.Split(seporator).Select(x => Int32.Parse(x));
            return new Vector3i(vector.ElementAt(0), vector.ElementAt(1), vector.ElementAt(2));
        }

    }
}
