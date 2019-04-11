#Clound Life
Files Saas

## Testing controllers
1) Add Route attribute and TestController attribute to the class.

	  [Route("api/[controller]")]
    [TestController]
    [ApiController]
    public class TestController : ControllerBase
    {
    }

2) Write action and add the action Route attribute, make sure the action returns bool(true = test successed, false = test failed).

    [HttpGet]
    [Route("userrepo")]
    public async Task<ActionResult<bool>> UserRepoTest()
    {
    	Random rnd = new Random();
    	return  rnd.Next(100) % 2 == 0;
    }

3) navigate to "/tests" route and check out your test.
