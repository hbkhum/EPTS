using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities;

namespace EPTS.Repositories.XML.Devices.Repositories
{
    [XmlRoot(ElementName = "Devices")]
    public class DataRepositories : IDataRepositories
    {
        private readonly  string _filelocation;
        private DataRepositories _dataRepositories;

        [XmlElement(ElementName = "SeaMaxDeviceRepository")]
        public SeaMaxDeviceRepository SeaMaxDeviceRepository { get; set; }
        [XmlElement(ElementName = "VisaComDeviceRepository")]
        public VisaComDeviceRepository VisaComDeviceRepository { get; set; }
        [XmlElement(ElementName = "SocketDeviceRepository")]
        public SocketDeviceRepository SocketDeviceRepository { get; set; }
        [XmlElement(ElementName = "ComDeviceRepository")]
        public ComDeviceRepository ComDeviceRepository { get; set; }

        public DataRepositories(string filelocation)
        {
            _filelocation = filelocation;
            var deserializer = new XmlSerializer(typeof(DataRepositories));
            var reader = new StreamReader(_filelocation);
            _dataRepositories = (DataRepositories) deserializer.Deserialize(reader);
            //ComRepository = _dataRepositories.ComRepository;
            SocketDeviceRepository = _dataRepositories.SocketDeviceRepository;
            VisaComDeviceRepository = _dataRepositories.VisaComDeviceRepository;
            SeaMaxDeviceRepository = _dataRepositories.SeaMaxDeviceRepository;
            reader.Close();

        }

        private DataRepositories(){}
        public void Save()
        {
            var serializerObj = new XmlSerializer(typeof(DataRepositories));
            var writeFileStream = new StreamWriter(_filelocation);
            serializerObj.Serialize(writeFileStream, _dataRepositories);
            writeFileStream.Close();
        }
    }


    public interface IDataRepositories
    {
        SeaMaxDeviceRepository SeaMaxDeviceRepository { get; set; }
        VisaComDeviceRepository VisaComDeviceRepository { get; set; }
        SocketDeviceRepository SocketDeviceRepository { get; set; }
        ComDeviceRepository ComDeviceRepository { get; set; }
    }

}
