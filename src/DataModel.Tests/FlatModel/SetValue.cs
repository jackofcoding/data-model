namespace DataModel.Tests.FlatModel;

public class SetValue
{
  [Test]
  public async Task SetValue_OnEmptyModel_AddsValue()
  {
    var model = new DataModel.FlatModel();
    model.SetValue("foo", "bar");

    var result = model.GetValue<string>("foo", out var success);
    
    await Assert.That(success).IsTrue();
    await Assert.That(result).IsEqualTo("bar");
  }
  
  [Test]
  public async Task SetValue_OnExistingKey_UpdatesValue()
  {
    var model = new DataModel.FlatModel();
    model.SetValue("foo", "bar");
    model.SetValue("foo", "baz");

    var result = model.GetValue<string>("foo", out var success);
    
    await Assert.That(success).IsTrue();
    await Assert.That(result).IsEqualTo("baz");
  }

  [Test]
  public async Task SetValue_DifferentValues_OnEmptyModel_AddsAllValues()
  {
    var model = new DataModel.FlatModel();
    model.SetValue("foo", "bar");
    model.SetValue("bar", "baz");

    var resultFoo = model.GetValue<string>("foo", out var successFoo);
    var resultBar = model.GetValue<string>("bar", out var successBar);
    
    await Assert.That(successFoo).IsTrue();
    await Assert.That(successBar).IsTrue();
    await Assert.That(resultFoo).IsEqualTo("bar");
    await Assert.That(resultBar).IsEqualTo("baz");
  }
  
  [Test]
  public async Task SetValue_WithNullKey_ThrowsArgumentNullException()
  {
    var model = new DataModel.FlatModel();

    await Assert.ThrowsAsync<ArgumentNullException>(LocalTestFunction);
    return;

    Task LocalTestFunction()
    {
      model.SetValue<string>(null!, "value");
      return Task.CompletedTask;
    }
  }
  
  [Test]
  public async Task SetValue_WithEmptyKey_ThrowsArgumentNullException()
  {
    var model = new DataModel.FlatModel();

    await Assert.ThrowsAsync<ArgumentNullException>(LocalTestFunction);
    return;

    Task LocalTestFunction()
    {
      model.SetValue<string>(String.Empty, "value");
      return Task.CompletedTask;
    }
  }

  [Test]
  [Arguments(" foo")]
  [Arguments(" foo")]
  [Arguments("\tfoo")]
  [Arguments("\nfoo")]
  [Arguments("foo ")]
  [Arguments("foo ")]
  [Arguments("foo\t")]
  [Arguments("foo\n")]
  public async Task SetValue_WithSurroundingWhitespaceKey_ThrowsArgumentException(string key)
  {
    var model = new DataModel.FlatModel();

    await Assert.ThrowsAsync<ArgumentException>(LocalTestFunction);
    return;

    Task LocalTestFunction()
    {
      model.SetValue<string>(key, "value");
      return Task.CompletedTask;
    }
  }
}
