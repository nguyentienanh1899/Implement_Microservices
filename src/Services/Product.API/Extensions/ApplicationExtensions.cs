namespace Product.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //The best way is to let the middleware authen, then routing, then authen, so that the middleware affects the order of execution when processing requests.
            app.UseAuthentication();
            app.UseRouting();

            //app.UseHttpsRedirection(); -> https for production only
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
