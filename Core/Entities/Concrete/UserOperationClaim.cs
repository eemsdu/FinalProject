namespace Core.Entities.Concrete
{
    public class UserOperationClaim:IEntity
    {
        //nesne tekil olur 
        public int  Id { get; set; }
        public int UserId { get; set; }  //bu hangi userın claimi
        public int OperationClaimId { get; set; } //bu hangi claim 

                                                  
    }
}
