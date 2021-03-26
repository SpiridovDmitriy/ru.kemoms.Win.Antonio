using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ru.kemoms.Sp.ANTONIO.XML
{
    public abstract class SpClass
    {
        protected internal System.IO.FileInfo f;
        protected internal string IdRule;
        protected internal System.Int64 IdPack;

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public List<object> _Err = new List<object>();
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string xmlns = "";

        public int Count { get { return this._Err.Count; } }

        protected int MaxErr = 1000;
        protected uint n_err = 0;

        public void SetFn(System.IO.FileInfo q)
        {
            this.f = q;
        }
        public void SetRule(string q)
        {
            this.IdRule = q;
        }
        public void SetIdPack(System.Int64 q)
        {
            this.IdPack = q;
        }

        public void SaveXML(string fn)
        {
            FileStream res = new System.IO.FileStream(fn, FileMode.Create);
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(this.GetType());
            #region Перекодировка из UTF-8 в windows 1251
            System.IO.StreamWriter file = new System.IO.StreamWriter(res, Encoding.GetEncoding(1251));
            if (this.xmlns == "")
            {
                System.Xml.Serialization.XmlSerializerNamespaces ns = new System.Xml.Serialization.XmlSerializerNamespaces();
                ns.Add("", "");

                writer.Serialize(file, this, ns);
            }
            else
            {
                writer.Serialize(file, this);
            }
            res.Close();
            #endregion
        }
        public MemoryStream XML
        {
            get
            {
                MemoryStream res = new System.IO.MemoryStream();
                System.Xml.Serialization.XmlSerializer writer =
                    new System.Xml.Serialization.XmlSerializer(this.GetType());
                #region Перекодировка из UTF-8 в windows 1251
                System.IO.StreamWriter file = new System.IO.StreamWriter(res, Encoding.GetEncoding(1251));
                if (this.xmlns == "")
                {
                    System.Xml.Serialization.XmlSerializerNamespaces ns = new System.Xml.Serialization.XmlSerializerNamespaces();
                    ns.Add("", "");

                    writer.Serialize(file, this, ns);
                }
                else
                {
                    writer.Serialize(file, this);
                }
                res.Position = 0;
                #endregion
                return res;
            }
        }

        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            this._Err.Add(new PR(this.n_err, "Строка " + e.Exception.LineNumber.ToString() + " Столбец " + e.Exception.LinePosition.ToString() + "\n" + e.Message));
        }
        private void Serializer_UnknownNode(object sender, XmlNodeEventArgs e) => _Err.Add(new PR(this.n_err, "Неизвестный узел:" + e.Name + "\t" + e.Text));
        private void Serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            this._Err.Add(new PR(this.n_err, "Неизвестный  атрибут " + e.Attr.Name + "='" + e.Attr.Value + "'"));
        }
        private void Serializer_UnknownElement(object sender, XmlElementEventArgs e) => this._Err.Add(new PR(this.n_err, "Неизвестный эллемент " + e.Element.Name + "='" + e.Element.InnerXml + "'"));

        public static object FromXml(Stream xmlMemory, Type ObjType, byte[] ObjXsd, byte[] ObjXsdType, byte[] ObjXsdVal)
        {
            Boolean Res = true;

            object obj = Activator.CreateInstance(ObjType);
            try
            {
                if (Res)
                {
                    if (xmlMemory != null && xmlMemory.Length > 0)
                    #region
                    {
                        XmlReaderSettings settings = new XmlReaderSettings
                        {
                            ValidationType = ValidationType.Schema
                        };
                        settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                        settings.ValidationEventHandler += new ValidationEventHandler((obj as SpClass).ValidationEventHandler);
                        XmlSchemaSet sc;
                        XmlReader reader;

                        Boolean ResStrXml = false;
                        Boolean ResStr = false;
                        Boolean ResVal = false;
                        Boolean ResType = false;

                        {
                            (obj as SpClass).n_err = 901;
                            xmlMemory.Position = 0;
                            reader = XmlReader.Create(xmlMemory);
                           // while (reader.Read() && (obj as SpClass)._Err.Count <= (obj as SpClass).MaxErr)
                            {
                            };

                            { }
                            ResStrXml = (obj as SpClass)._Err.Count == 0;
                        }


                        if (ObjXsd != null)
                        {
                            //sc = new XmlSchemaSet();
                            //sc.Add((obj as SpClass).xmlns, new XmlTextReader(new MemoryStream(ObjXsd)));
                            //settings.Schemas = sc;
                            //(obj as SpClass).n_err = 901;
                            //xmlMemory.Position = 0;
                            //reader = XmlReader.Create(xmlMemory, settings);
                            //while (reader.Read() && (obj as SpClass)._Err.Count <= 1000) ;
                            ResStr = (obj as SpClass)._Err.Count == 0;
                        }
                        else
                        {
                            ResStr = true;
                        }

                        if (ObjXsdVal != null)
                        {
                            sc = new XmlSchemaSet();
                            sc.Add((obj as SpClass).xmlns, new XmlTextReader(new MemoryStream(ObjXsdVal)));
                            settings.Schemas = sc;
                            (obj as SpClass).n_err = 902;
                            xmlMemory.Position = 0;
                            reader = XmlReader.Create(xmlMemory, settings);
                            while (reader.Read() && (obj as SpClass)._Err.Count <= 1000) ;
                            ResVal = (obj as SpClass)._Err.Count == 0;
                        }
                        else
                        {
                            ResVal = true;
                        }

                        if (ObjXsdType != null)
                        {
                            sc = new XmlSchemaSet();
                            sc.Add((obj as SpClass).xmlns, new XmlTextReader(new MemoryStream(ObjXsdType)));
                            settings.Schemas = sc;
                            (obj as SpClass).n_err = 903;
                            xmlMemory.Position = 0;
                            reader = XmlReader.Create(xmlMemory, settings);
                            while (reader.Read() && (obj as SpClass)._Err.Count <= 1000) ;
                            ResType = (obj as SpClass)._Err.Count == 0;
                        }
                        else
                        {
                            ResType = true;
                        }
                        Res = (Res && ResStrXml && ResStr && ResVal && ResType);
                    }
                    if (Res)
                    {
                        xmlMemory.Position = 0;
                        System.Xml.Serialization.XmlSerializer reader =
                                new System.Xml.Serialization.XmlSerializer(ObjType);
                        reader.UnknownNode += new XmlNodeEventHandler((obj as SpClass).Serializer_UnknownNode);
                        reader.UnknownAttribute += new XmlAttributeEventHandler((obj as SpClass).Serializer_UnknownAttribute);
                        reader.UnknownElement += new XmlElementEventHandler((obj as SpClass).Serializer_UnknownElement);
                        obj = reader.Deserialize(xmlMemory);
                    }
                }
                else
                {
                    (obj as SpClass)._Err.Add(new PR(901, "Размер XML файла равен 0"));
                }
                #endregion
            }
            catch (System.Xml.XmlException e)
            {
                Res = false;
                (obj as SpClass)._Err.Add(new PR((obj as SpClass).n_err, e.Message));
            }

            return obj;
        }
        //public abstract Boolean InsOra(OracleTransaction tx);
    }
}
