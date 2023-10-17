using AutoMapper;
using TreasureMap.Dtos;
using TreasureMap.Entities;

namespace TreasureMap.Mappers
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region (CellDto) --> (Cell)

            CreateMap<CellDto, Cell>();

            #endregion

            #region (AdventurerDto) --> (Adventurer)

            CreateMap<AdventurerDto, Adventurer>();

            #endregion

        }
    }
    }
