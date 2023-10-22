using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Dtos;
using TreasureMap.Entities;
using TreasureMap.Enums;
using TreasureMap.ValueObjects;

namespace TreasureMap.Extensions
{
    internal static class StringExtensions
    {
        public static readonly Dictionary<char, Func<string, object>> keyValuePairs = new Dictionary<char, Func<string, object>> 
            {
            {'C', (line)=>  { return new MapDto()
                {
                    Height = line.GetDigitFromString(Constants.separator,1),
                    Width= line.GetDigitFromString(Constants.separator,2)
                };
            } 
            },
            {'T', (line)=> { return Enumerable.Range(0,line.GetDigitFromString(Constants.separator,3))
                    .Select(i => new TreasureDto(){
                        X= line.GetDigitFromString(Constants.separator,2),
                        Y= line.GetDigitFromString(Constants.separator,1)
                    } ).ToList(); } },
            {'M', (line)=> { return new MountainDto()
                {
                    X= line.GetDigitFromString(Constants.separator,2),
                    Y= line.GetDigitFromString(Constants.separator,1)
                }; 
            } 
            },
            {'A', (line)=> { return new AdventurerDto()
                {
                    Name= line.GetSubStringFromString(Constants.separator,1),
                    Direction= (Direction) line.GetCharFromString(Constants.separator,4),
                    X= line.GetDigitFromString(Constants.separator,3),
                    Y= line.GetDigitFromString(Constants.separator,2),
                    Movements= line.GetSubStringFromString(Constants.separator,5)
                }; 
            } 
            },
            {'#', (line)=> { return null; } }
            };

        public static int GetDigitFromString(this String str, string separator, int index)
        {
            return Int32.Parse(str.Split(separator)[index]);
        }

        public static char GetCharFromString(this String str, string separator, int index)
        {
            return Char.Parse(str.Split(separator)[index]);
        }

        public static string GetSubStringFromString(this String str,string separator, int index)
        {
            return str.Split(separator)[index];
        }

        public static object CreateObjectFromString(this String str)
        {
            try
            {
                return keyValuePairs.GetValueOrDefault(str.ToCharArray()[0]).Invoke(str);
            }
            catch (ArgumentNullException e)
            {
                return null;
            }
            
        }

      
    }
}
