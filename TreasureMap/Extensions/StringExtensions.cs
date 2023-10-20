using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Dtos;
using TreasureMap.Entities;
using TreasureMap.Enums;

namespace TreasureMap.Extensions
{
    internal static class StringExtensions
    {
        public static readonly Dictionary<char, Func<string, object>> keyValuePairs = new Dictionary<char, Func<string, object>> 
            {
            {'C', (line)=>  { return new MapDto()
                {
                    Height = line.GetDigitFromString(2),
                    Width= line.GetDigitFromString(4)
                };
            } 
            },
            {'T', (line)=> { return Enumerable.Range(0, Convert.ToInt32(line.ToCharArray()[6]))
                    .Select(i => new TreasureDto(){
                        X= line.GetDigitFromString(2),
                        Y= line.GetDigitFromString(4)
                    } ).ToList(); } },
            {'M', (line)=> { return new MountainDto()
                {
                    X= line.GetDigitFromString(2),
                    Y= line.GetDigitFromString(4)
                }; 
            } 
            },
            {'A', (line)=> { return new AdventurerDto()
                {
                    Name= line.GetSubStringFromString("-",1),
                    Direction= (Direction) line.GetDigitFromString(4),
                    X= line.GetDigitFromString(5),
                    Y= line.GetDigitFromString(7),
                    Movements= line.GetSubStringFromString("-",5)
                }; 
            } 
            },
            {'#', (line)=> { return null; } }
            };

        public static int GetDigitFromString(this String str,int index)
        {
            return Convert.ToInt32(str.ToCharArray()[index]);
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
