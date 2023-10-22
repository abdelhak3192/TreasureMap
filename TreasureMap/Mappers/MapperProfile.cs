using AutoMapper;
using TreasureMap.Dtos;
using TreasureMap.Entities;

namespace TreasureMap.Mappers
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {

            #region (TreasureDto) --> (Treasure)

            CreateMap<TreasureDto, Treasure>();

            #endregion

            #region (MountainDto) --> (Mountain)

            CreateMap<MountainDto, Mountain>();

            #endregion
            #region (CellDto) --> (Cell)

            CreateMap<CellDto, Cell>();

            #endregion

            #region (AdventurerDto) --> (Adventurer)

            CreateMap<AdventurerDto, Adventurer>();

            #endregion

            #region (MapDto) --> (Map)

            CreateMap<MapDto, Map>();

            #endregion




        }

    }
    }
