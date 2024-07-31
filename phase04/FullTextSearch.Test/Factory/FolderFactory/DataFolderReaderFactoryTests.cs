﻿using phase02.Document;
using phase02.Exceptions;
using phase02.Factory.FolderFactory;

namespace FullTextSearch.Test.Factory.FolderFactory;

public class DataFolderReaderFactoryTests
{
    private readonly DataFolderReaderFactory _sut;

    public DataFolderReaderFactoryTests()
    {
        _sut = new DataFolderReaderFactory([new DocumentFolderReader()]);
    }

    [Fact]
    public void MakeDataFolderReader_ShouldReturnsCorrectDataFolderReader_WhenClassNameIsOk()
    {
        //Arrange
        var className = DataType.Document;
        var dataFolderReader = new DocumentFolderReader();

        //Act
        var result = _sut.MakeDataFolderReader(className);

        //Assert
        Assert.Equal(result.GetType(), dataFolderReader.GetType());
    }
    
    [Fact]
    public void MakeDataFolderReader_ShouldReturnsInvalidClassNameException_WhenClassNameIsNotOk()
    {
        //Arrange
        var className = DataType.WrongType;

        //Act
        var action = () => _sut.MakeDataFolderReader(className);

        //Assert
        Assert.Throws<InvalidClassName>(action);
    }
}