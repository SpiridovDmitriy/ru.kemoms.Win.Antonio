using System;
using System.Collections.Generic;
namespace ru.kemoms.Sp.ANTONIO.XML
{
    // 
    // Этот исходный код был создан с помощью xsd, версия=4.0.30319.1.
    // 

    /// <remarks/>
    /// <summary>
    /// Реализация базового класса FLK_P для скорой помощи
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    //
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false)]
    public partial class FLK_P : SpClass
    {
        protected internal string vERSField;

        protected internal string fNAMEField;

        protected internal string fNAME_IField;

        private PR[] prField;
        public FLK_P() { }
        public FLK_P(string f1, string f2, List<object> e1, List<object> e2)
        {
            this.fNAMEField = f1;
            this.fNAME_IField = f2;
            int l1 = e1.Count;
            int l2 = e2.Count;
            this.prField = new PR[l1 + l2];
            for (int j = 0; j < l1 + l2; j++)
            {
                this.prField[j] = j < l1 ? e1[j] as PR : e2[j - l1] as PR;
            }

        }
        public static string Fn(string q)
        { return "FLK_" + q; }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string VERS
        {
            get
            {
                return this.vERSField;
            }
            set
            {
                this.vERSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string FNAME
        {
            get
            {
                return this.fNAMEField;
            }
            set
            {
                this.fNAMEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string FNAME_I
        {
            get
            {
                return this.fNAME_IField;
            }
            set
            {
                this.fNAME_IField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PR", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public PR[] PR
        {
            get
            {
                return this.prField;
            }
            set
            {
                this.prField = value;
            }
        }
        //public override Boolean InsOra(Oracle.ManagedDataAccess.Client.OracleTransaction tx)
        //{
        //    return false;
        //}
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    // 
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PR
    {

        private uint oSHIBField;

        private string iM_POLField;

        private string bAS_POLField;

        private string n_RECField;

        private string cOMMENTField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public uint OSHIB
        {
            get
            {
                return this.oSHIBField;
            }
            set
            {
                this.oSHIBField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string IM_POL
        {
            get
            {
                return this.iM_POLField;
            }
            set
            {
                this.iM_POLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string BAS_POL
        {
            get
            {
                return this.bAS_POLField;
            }
            set
            {
                this.bAS_POLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string N_REC
        {
            get
            {
                return this.n_RECField;
            }
            set
            {
                this.n_RECField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string COMMENT
        {
            get
            {
                return this.cOMMENTField;
            }
            set
            {
                this.cOMMENTField = value;
            }
        }

        public PR()
        { }
        public PR(uint PoSHIB, string PcOMMENT)
        {
            oSHIBField = PoSHIB;
            cOMMENTField = PcOMMENT;
        }
        public string spToString()
        {
            string otv;
            otv =
                oSHIBField.ToString() + ":" +
                this.bAS_POLField != null ? this.BAS_POL : "" + ":" +
                this.iM_POLField != null ? this.IM_POL : "" + ":" +
                this.n_RECField != null ? this.N_REC : "" + ":" +
                this.cOMMENTField != null ? this.COMMENT : ""
                ;
            return otv;
        }

    }
}
