using AppShared.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;



namespace AppShared.Models
{
    public interface IBasicEntity
    {
        [Key]
        [Display(Name = "ИД")]
        Guid Id { get; set; }
        [Display(Name = "Создан")]
        DateTime Created { get; set; }
        [Display(Name = "Изменен")]
        DateTime Modified { get; set; }


    }

    public interface IBasicUserEntity : IBasicEntity
    {
        [ForeignKey(nameof(AppShared.Models.User))]
        Guid UserId { get; set; }
        User User { get; set; }
    }

    public abstract class BasicEntityNonUser : IBasicEntity
    {
        [Key]
        [Display(Name = "ИД")]
        public Guid Id { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Создан")]

        public virtual DateTime Created { get; set; }

        [Display(Name = "Изменен")]
        [DataType(DataType.DateTime)]
        public virtual DateTime Modified { get; set; }

        //Из за этого AntDesign постоянно перезагружается
        //public override bool Equals(object obj)
        //{
        //    if (obj != null && obj is BasicEntity entity)
        //    {

        //        if (this.Id == entity.Id)
        //            return true;
        //        else
        //            return base.Equals(obj);
        //    }
        //    return base.Equals(obj);
        //}

        //public override int GetHashCode()
        //{
        //    return this.Id.GetHashCode();
        //}

    }

    public abstract class BasicEntity : BasicEntityNonUser, IBasicUserEntity
    {

        [ForeignKey(nameof(AppShared.Models.User))]
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }

    }


    public abstract class SimpleEntity : BasicEntity
    {
        //public string Status { get; set; }
        public virtual string Type { get; set; }
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public virtual string Value { get; set; }


    }

    public interface HTMLDrawableItem
    {
        void DrawHTML();
    }

    public interface IEmitableEntity
    {
        void Create();
        void Update();
        void Modify();
        void Remove();
    }

    public interface IEmitableEntityArray : IEmitableEntity
    {
        void Append();
        void Prepend();

    }

    /// <summary>
    /// ����� ��������� ����� �������� ������ � �������� � �������� ��� ���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICascadeIterator<T>
    {

    }

    /// <summary>
    /// ��������� ��� ������ ����� �� ����������� ��� �������
    /// </summary>
    public interface IOperatable
    {

    }

    public interface DT_Query<T1, T2>
    {

    }


    interface IModificator
    {
        // void Create
    }

    enum Z
    {
        Create,
        Update
    }
}