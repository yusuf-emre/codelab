using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Repositories;
using System.Linq;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsuranceController : Controller
    {
        private IRepository<Insurance> _repo;
        public InsuranceController(IRepository<Insurance> repo)
        {
            _repo = repo;
        }
        [HttpGet("top/{maxCount}/{maxDepth}")]
        public ActionResult<int []> Top(int maxCount, int maxDepth)
        {
            var insuranceList = _repo.GetAll();
            var allCombinedValues = new int[insuranceList.Count()];
            var i = 0;
        
            foreach(var insurance in insuranceList)
            {
                insurance.Depth = 0;
                var sum = 0;
                sum += insurance.Value;

                if(insurance.Children.Count() == 0 || insurance.Depth == maxDepth)
                {
                    allCombinedValues[i] = sum;
                    i++;
                    continue;
                }

                _repo.LoopChildren(insurance, sum, allCombinedValues, i, maxDepth);

                i++;
            }

            var topValues = allCombinedValues.OrderByDescending(x => x).Take(maxCount).ToArray();
            return Ok(topValues);
        }
    }
}