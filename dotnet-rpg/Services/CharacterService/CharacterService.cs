using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>()
        {
            new Character() {Id = 2, Name = "behzad", Class = RpgClass.Cleric , Defense = 2, HitPoints = 2,  Intelligence = 9, Strength = 12},
            new Character{ Id = 1,Name = "Sam", Class = RpgClass.Mage , Defense = 12, HitPoints = 41,  Intelligence = 2, Strength = 22},
            new Character{ Id = 3,Name = "Hasan", Class = RpgClass.Knight, Defense = 22, HitPoints = 41,  Intelligence = 2, Strength = 22},
            // = new Character() { };
        };
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper,DataContext context,IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {

            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);

            character.Users = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            
            var answerfromDb = _context.Character.Add(character);
            //characters.Add(_mapper.Map<Character>(newCharacter));
            await _context.SaveChangesAsync();
            serviceResponse.Data = await  _context.Character.Where(c => c.Users.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                Character character = await _context.Character.FirstOrDefaultAsync(x => x.Id == id && x.Users.Id == GetUserId());
                if(character != null)
                {
                    _context.Character.Remove(character);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Character.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character Not Found";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        //int userId
        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter()
        {
            //var serviceResponse = new ServiceResponse<List<GetCharacterDto>>() { Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList() };
            //return serviceResponse;
            var response = new ServiceResponse<List<GetCharacterDto>>();

            List<Character> dbCharacters = await _context.Character
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.Users.Id == GetUserId()).ToListAsync();
            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;

        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacters = await _context.Character
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.Users.Id == GetUserId());
            var character = _context.Character.FirstOrDefaultAsync(x => x.Id == id && x.Users.Id == GetUserId());
                //characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }

        public  async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            Character charcter = _context.Character.Include(x => x.Users).FirstOrDefault(c => c.Id == updateCharacter.Id);

            try
            {
                // _mapper.Map(updateCharacter, charcter);
                if (charcter.Users.Id == GetUserId())
                {
                    charcter.Name = updateCharacter.Name;
                    charcter.HitPoints = updateCharacter.HitPoints;
                    charcter.Strength = updateCharacter.Strength;
                    charcter.Defense = updateCharacter.Defense;
                    charcter.Intelligence = updateCharacter.Intelligence;
                    charcter.Class = updateCharacter.Class;
                    response.Data = _mapper.Map<GetCharacterDto>(charcter);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Character Not Found";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
               
            }
            return response;

        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Character.Include(c => c.Weapon).Include(c => c.Skills).FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.Users.Id == GetUserId());

                if (character == null)
                {
                    response.Success = false;
                    response.Message = " Character not Found";
                    return response;
                }
                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = "Skil Not Found";
                    return response;
                }

                character.Skills.Add(skill);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }


    }
}
