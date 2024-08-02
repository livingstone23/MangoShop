


namespace MangoShop.Domain.Models;



/// <summary>
/// Base clase for domain entities
/// </summary>
public class Entity
{

    /// <summary>
    /// Field for present the id of the entity
    /// </summary>
    /// <value></value>
    public int Id { get; set; }



    //Control fields
    
    /// <summary>
    /// Field for register the creation date of the entity
    /// </summary>
    /// <value></value>
    public DateTime? Created { get; set; }

    /// <summary>
    /// Field for register the user that created the entity
    /// </summary>
    /// <value></value>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Field for register the last update date of the entity
    /// </summary>
    /// <value></value>
    public DateTime? Updated { get; set; }

    /// <summary>
    /// Field for register the user that updated the entity
    /// </summary>
    /// <value></value>
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Field for register the deletion date of the entity
    /// </summary>
    /// <value></value>
    public int? GcRecord { get; set; }

}

