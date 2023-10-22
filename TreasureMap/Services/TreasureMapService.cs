using AutoMapper;
using System.Threading;
using TreasureMap.Dtos;
using TreasureMap.Entities;
using TreasureMap.Enums;
using TreasureMap.Interfaces;
using TreasureMap.ObjectCreators;
using TreasureMap.ValueObjects;

namespace TreasureMap.Services
{
    internal class TreasureMapService : ITreasureMapService
    {
        private readonly IImportDataService _importDataService;
        private readonly IMapper _mapper;
        private int _adventurerPrirority { get; set; } = 0;
        private Map _treasureMap;

        public TreasureMapService(
            IImportDataService importDataService,
            IMapper imapper)
        {
            _importDataService = importDataService;
            _mapper = imapper;
        }

        
        public void Play()
        {
            IList<object> dtos=_importDataService.ImportDataFromFile(Constants.TreasureFilePath);
            MapDto filledMapDto=FillMap(dtos);
            _treasureMap=_mapper.Map<Map>(filledMapDto);
            IList<Adventurer> allAdventurers = _treasureMap.Cells.Where(c => c.Adventurers != null && c.Adventurers.Any()).SelectMany(c => c.Adventurers).ToList();
            CreateAdventurerMovementCreators(allAdventurers);
            MoveAdventurers(allAdventurers);

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
                if (obj is AdventurerDto)
                    ((AdventurerDto)obj).Prirority = _adventurerPrirority++;
                _mapToFillData.GetValueOrDefault(obj.GetType())?.Invoke(obj, mapToFill);  
            }

            return mapToFill;
        }
        private readonly Dictionary<char, MovementCreator> _movementsCreators = new Dictionary<char, MovementCreator>()
        {
            {'A', new SimpleAdvenceMovementCreator() },
            {'D', new SimpleRightMovementCreator() } ,
            {'G', new SimpleLeftMovementCreator() } ,
        };
        
        private void CreateAdventurerMovementCreators(IList<Adventurer> adventurers)
        {
            foreach(Adventurer adventurer in adventurers)
            {
                adventurer.MovementCreators=new Queue<IMovementCreator>();
                foreach (char mov in adventurer.Movements)
                {
                    adventurer.MovementCreators.Enqueue(_movementsCreators.GetValueOrDefault(mov));
                }
            }
        }

        private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private void MoveAdventurers(IList<Adventurer> adventurers)
        {
            Queue<Adventurer> adventurersQueue= new Queue<Adventurer>(adventurers.OrderBy(a => a.Prirority));
            while (adventurersQueue.Count>0)
            {
                Adventurer adventurer=adventurersQueue.Dequeue();
                _semaphore.Wait();
                Console.WriteLine($"Moving adventurer {adventurer.Name}");
                (int,int,Direction) position= adventurer.MovementCreators.Dequeue().Move((adventurer.X,adventurer.Y),adventurer.Direction);
                if(IsMovementValid(position.Item1, position.Item2))
                {
                    ValidateMovement(position.Item1, position.Item2, position.Item3, adventurer);
                    // Print the current position of the adventurer
                    Console.WriteLine($"New position for {adventurer.Name}: ({adventurer.X},{adventurer.Y})");
                }
                if(adventurer.MovementCreators.Count > 0) 
                    adventurersQueue.Enqueue(adventurer);
                _semaphore.Release();
            }
        }
        private bool IsMovementValid(int x,int y)
        {

            return x>0 && 
                x<_treasureMap.Width && 
                y>0 && 
                y<_treasureMap.Height && 
                !_treasureMap.Cells.Any(c=>c.isMountainous || c.Adventurers.Any());
        }
        
        private void ValidateMovement(int x,int y,Direction direction,Adventurer adventurer)
        {
            _treasureMap.Cells.FirstOrDefault(c=>c.X==adventurer.X && c.Y==adventurer.Y).Adventurers.Remove(adventurer);
            adventurer.X = x; adventurer.Y=y;
            adventurer.Direction = direction;
            _treasureMap.Cells.FirstOrDefault(c => c.X == y && c.Y == y).Adventurers.Add(adventurer);


        }
        #endregion 

    }
}
