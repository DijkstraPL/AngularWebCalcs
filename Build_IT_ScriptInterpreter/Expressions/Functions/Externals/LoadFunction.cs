using Build_IT_ScriptInterpreter.Expressions.Functions.Interfaces;
using NCalc;
using System;
using System.Composition;

namespace Build_IT_ScriptInterpreter.Expressions.Functions.Externals
{
    [Export(typeof(IFunction))]
    public class LoadFunction : IFunction
    {
        #region Properties

        public string Name { get; private set; }
        public Func<FunctionArgs, object> Function { get; private set; }

        #endregion // Properties

        //#region Fields

        //private readonly ICategoryRepository _categoryRepository;
        //private readonly ISubcategoryRepository _subcategoryRepository;
        //private readonly IMaterialRepository _materialRepository;

        //private readonly IDictionary<string, Func<string, Task<object>>> _repositories
        //    = new Dictionary<string, Func<string, Task<object>>>();

        //#endregion // Fields

        //#region Constructors

        //public LoadFunction()
        //{
        //    Set();
        //    PopulateRepositories();
        //}

        //internal LoadFunction(
        //    ICategoryRepository categoryRepository = null,
        //    ISubcategoryRepository subcategoryRepository = null,
        //    IMaterialRepository materialRepository = null) : this()
        //{
        //    _categoryRepository = categoryRepository;
        //    _subcategoryRepository = subcategoryRepository;
        //    _materialRepository = materialRepository;
        //}

        //#endregion // Constructors

        //#region Private_Methods

        //private void Set()
        //{
        //    Name = "LOAD";
        //    Function = (e) =>
        //    {
        //        string table = e.Parameters[0].Evaluate().ToString();
        //        string value = e.Parameters[1].Evaluate().ToString();
        //        if (_repositories.ContainsKey(table))
        //            return _repositories[table](value).Result;
        //        throw new ArgumentException("Wrong database name");
        //    };
        //}

      //  private void PopulateRepositories()
      //  {
       //     //_repositories.Add("DeadLoad-Category", async value =>
       //     //{
       //     //    using (var deadLoadsDbContext = new DeadLoadsDbContext(new DbContextOptions<DeadLoadsDbContext>()))
       //     //    {
       //     //        var categoryRepository = _categoryRepository ?? new CategoryRepository(deadLoadsDbContext);
       //     //        var categories = await _categoryRepository.GetAllAsync();
       //
       //     //        var selectedCategories = categories.Where(c => c.Name == value);
       //
       //     //        return selectedCategories.SelectMany(sc => sc.Subcategories.Select(s => s.Name));
       //     //    }
       //     //});
      //  }

       // #endregion // Private_Methods
    }
}
