namespace DataModel.Tests.FlatModel;

// Note: basic tests are already covered in SetValue
public class GetValue
{
  [Test]
  public async Task GetValue_OnEmptyModel_ReturnsDefaultAndSuccessFalse()
  {
    var model = new DataModel.FlatModel();
    var result = model.GetValue<string>("foo", out var success);

    await Assert.That(success).IsFalse();
    await Assert.That(result).IsNull();
  }

  [Test]
  public async Task GetValue_OnWrongType_ReturnsDefaultAndSuccessFalse()
  {
    var model = new DataModel.FlatModel();
    model.SetValue("foo", "bar");

    var result = model.GetValue<int>("foo", out var success);

    await Assert.That(success).IsFalse();
    await Assert.That(result).IsEqualTo(0);
  }

  [Test]
  public async Task GetValue_OnCastableMoreSpecificType_Numbers_ReturnsDefaultAndSuccessFalse()
  {
    var model = new DataModel.FlatModel();
    model.SetValue("foo", 123);

    var result = model.GetValue<short>("foo", out var success);

    await Assert.That(success).IsFalse();
    await Assert.That(result).IsEqualTo((short)0);
  }

  [Test]
  public async Task GetValue_OnCastableLessSpecificType_Numbers_ReturnsDefaultAndSuccessFalse()
  {
    var model = new DataModel.FlatModel();
    model.SetValue("foo", (short)123);

    var result = model.GetValue<int>("foo", out var success);

    await Assert.That(success).IsFalse();
    await Assert.That(result).IsEqualTo(0);
  }
  
  [Test]
  public async Task GetValue_OnCastableMoreSpecificType_Class_ReturnsDefaultAndSuccessFalse()
  {
    var model = new DataModel.FlatModel();
    model.SetValue("foo", new Parent());

    var result = model.GetValue<Child>("foo", out var success);

    await Assert.That(success).IsFalse();
    await Assert.That(result).IsEqualTo(null);
  }

  [Test]
  public async Task GetValue_OnCastableLessSpecificType_Class_ReturnsValueAndSuccessTrue()
  {
    var model = new DataModel.FlatModel();
    var value = new Child();
    model.SetValue("foo", value);

    var result = model.GetValue<Parent>("foo", out var success);

    await Assert.That(success).IsTrue();
    await Assert.That(result).IsEqualTo(value);
  }

  private class Parent;
  private class Child : Parent;
}
