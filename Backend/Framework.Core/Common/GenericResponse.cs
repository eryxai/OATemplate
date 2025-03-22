#region Using ...
using System;
using System.Collections.Generic;
#endregion

/*
 
 
 */
namespace Framework.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericResponse<T>
	{
		#region Data Members

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance from type 
		/// GenericResponse.
		/// </summary>
		public GenericResponse()
			: this(null)
		{

		}
		/// <summary>
		/// Initializes a new instance from type 
		/// GenericResponse.
		/// </summary>
		/// <param name="instructionSetCollection"></param>
		public GenericResponse(ICollection<InstructionSetDelegate> instructionSetCollection)
		{
			this.Exceptions = new List<Exception>();
			this.InstructionSets = new List<InstructionSetDelegate>(instructionSetCollection);
		}
		#endregion

		#region Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="instructionSet"></param>
		public void Process(InstructionSetDelegate instructionSet)
		{
			this.InstructionSets.Add(instructionSet);
			this.Process();
		}
		/// <summary>
		/// 
		/// </summary>
		public void Process()
		{
			foreach (var step in InstructionSets)
			{
				try
				{
					step();
				}
				catch (Exception ex)
				{
					this.Exceptions.Add(ex);
				}
			}
		} 
		#endregion


		#region Properties
		public IList<InstructionSetDelegate> InstructionSets { get; set; }
		public T Result { get; set; }
		public IList<Exception> Exceptions { get; set; } 
		#endregion
	}
}
