namespace TicTacToeBlazorServer.Middlewares
{
    public class UniqueIdMiddleware 
    {
        private readonly RequestDelegate next;

        public UniqueIdMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Cookies.ContainsKey("UserId"))
            {
                string uniqueId = Guid.NewGuid().ToString();
                context.Response.Cookies.Append("UserId", uniqueId);
            }
            await next(context);
        }
    }
}
