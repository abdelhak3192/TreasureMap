using AutoMapper;
using TreasureMap.Dtos;
using TreasureMap.Entities;
using TreasureMap.Interfaces;
using TreasureMap.ValueObjects;

namespace TreasureMap.Services
{
    internal class TreasureMapService : ITreasureMapService
    {
        private readonly IImportDataService _importDataService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IMovementCreator> _movementCreators;

        public TreasureMapService(
            IImportDataService importDataService,
            IMapper imapper,
            IEnumerable<IMovementCreator> movementCreators)
        {
            _importDataService = importDataService;
            _mapper = imapper;
            _movementCreators = movementCreators;
        }
        public void Play()
        {
            IList<object> dtos=_importDataService.ImportDataFromFile(Constants.TreasureFilePath);
            MapDto filledMapDto=FillMap(dtos);
            Map treasureMap=_mapper.Map<Map>(filledMapDto);
            // Algorithme

        }

        #region private functions
        private readonly Dictionary<Type, Func<object,MapDto,MapDto>> _mapToFillData= new Dictionary<Type, Func<object, MapDto,MapDto>> ()
        {
            { typeof(MapDto),(object map,MapDto mapToFill) =>
                {
                    mapToFill.Cells =new List<CellDto>();
                    mapToFill.Width = ((MapDto)map).Width;
                    mapToFill.Height = ((MapDto)map).Height;
                    return mapToFill;
                }
            },
            { typeof(List<TreasureDto>),(object objects,MapDto mapToFill) =>
                {
                    TreasureDto firstTreasure=((IList<TreasureDto>)objects).FirstOrDefault();
                    if(firstTreasure==default)
                        return mapToFill;
                    int x=firstTreasure.X,y=firstTreasure.Y;
                    IList<TreasureDto> treasures=(IList<TreasureDto>)objects;
                    if(mapToFill.Cells.Any(c=>c.X==x && c.Y==y))
                        ((List<TreasureDto>)(mapToFill.Cells.First<CellDto>(c=>c.X==x && c.Y==y).Treasures)).AddRange(treasures);
                    else

                    mapToFill.Cells.Add(new CellDto()
                            {
                                X=x,
                                Y=y,
                                Treasures=treasures,
                            });

                    return mapToFill;
                }
            },
            {typeof(MountainDto), (object obj,MapDto mapToFill)=>
                {
                    MountainDto mountain=(MountainDto)obj;
                    int x=mountain.X,y=mountain.Y;
                    if(mapToFill.Cells.Any(c=>c.X==x && c.Y==y))
                    {
                        mapToFill.Cells.First<CellDto>(c=>c.X==x && c.Y==y).IsMountainous=true;
                        mapToFill.Cells.First<CellDto>(c=>c.X==x && c.Y==y).Mountain=(MountainDto)mountain;
                    }
                        
                    else
                    {

                            mapToFill.Cells.Add(new CellDto()
                        {
                            X=x,
                            Y=y,
                            IsMountainous=true,
                            Mountain=mountain,
                        });
                    }
                    
                    return mapToFill;
                } 
            },
            { typeof(AdventurerDto),(object obj,MapDto mapToFill)=>
                {
                    AdventurerDto adventurer=(AdventurerDto) obj;
                    int x=adventurer.X,y=adventurer.Y;
                    if(mapToFill.Cells.Any(c=>c.X==x && c.Y==y))
                       mapToFill.Cells.First<CellDto>(c=>c.X==x && c.Y==y).Adventurers.Add(adventurer);
                    else 
                     mapToFill.Cells.Add(new CellDto()
                        {
                            X=x,
                            Y=y,
                            Adventurers=new List<AdventurerDto>()
                            { adventurer },
                        });
                    return mapToFill;
                }
            }

        };
        private MapDto FillMap(IList<object> objects)
        {
            MapDto mapToFill=new MapDto();
            foreach (object obj in objects)
            {
                Console.WriteLine(obj.GetType());
                _mapToFillData.GetValueOrDefault(obj.GetType())?.Invoke(obj, mapToFill);
            }

            return mapToFill;
        }
        //private void CreateAdventurerMovements(out Adventurer adventurer)
        //{

        //}
        #endregion 

    }
}
