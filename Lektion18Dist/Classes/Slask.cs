namespace Lektion18Dist.Classes
{
    public class Slask
    {
        List<string>? SlaskList = null;
        public void TestaFel(string value)
        {
            try 
            {
                SlaskList.Add(value);
            }
            catch(ArgumentNullException ex)
            {
                SlaskList = new();
                SlaskList.Add(value);
            }
            catch
            {
                throw;
            }
        }
    }
}
