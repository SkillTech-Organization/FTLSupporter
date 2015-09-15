using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PMap.Common
{


    public class DisplayNameAttributeX : DisplayNameAttribute
    {
        public string Name {get;set;}
        public int Order {get;set;}

        

        public DisplayNameAttributeX()
            : base()
        {
        }

        public DisplayNameAttributeX(string p_name)
            :base( p_name)
        {
            Name = p_name;
        }
        public DisplayNameAttributeX(string p_name, int p_order)
            : base(p_name)
        {
            Name = p_name;
            Order = p_order;
        }
        public override string DisplayName
        {
            get
            {
                return  Order.ToString().PadLeft(2,' ') + "\t" + Name;
            }
        }

    }

    [AttributeUsage(AttributeTargets.All)]
    public class EditorX : Attribute
    {
        private Type m_editorType;
        private object[] m_parameterlist;

        public EditorX(Type p_editorType, params object[] p_parameterlist)
        {
            m_editorType = p_editorType;
            m_parameterlist = p_parameterlist;
        }
        public Type EditorType
        {
            get { return m_editorType; }
        }
        public object[] Parameterlist
        {
            get { return m_parameterlist; }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyOrderX : Attribute
    {
        private int order;

        public PropertyOrderX(int order)
        {
            this.order = order;
        }

        public int Order
        {
            get { return this.order; }
        }
    }

 

}
