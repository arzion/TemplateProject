using System;
using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace MailSender.Common
{
	/// <summary>
	/// Provides the global functionality for resolving dependencies using IOC container.
	/// </summary>
	public static class ComponentLocator
	{
		private static readonly IWindsorContainer WindsorContainer;

		/// <summary>
		/// Initializes static members of the <see cref="ComponentLocator"/> class.
		/// </summary>
		static ComponentLocator()
		{
			WindsorContainer = new WindsorContainer();
		}

		/// <summary>
		/// Gets the current IOC container.
		/// </summary>
		public static IWindsorContainer Container
		{
			get { return WindsorContainer; }
		}

		/// <summary>
		/// Gets the component instance for specific type.
		/// </summary>
		/// <typeparam name="T">Type to resolve.</typeparam>
		/// <returns>The resolved type.</returns>
		public static T GetComponent<T>()
			where T : class
		{
			return (T)GetComponent(typeof(T));
		}

		/// <summary>
		/// Gets the component instance for specific type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>The resolved type.</returns>
		public static object GetComponent(Type type)
		{
			return WindsorContainer.Resolve(type);
		}

		/// <summary>
		/// Gets all component instances that registered for specific type.
		/// </summary>
		/// <typeparam name="T">Type to resolve.</typeparam>
		/// <returns>The resolved types.</returns>
		public static T[] GetAllComponents<T>()
			where T : class
		{
			return (T[])GetAllComponents(typeof(T));
		}

		/// <summary>
		/// Gets all component instances that registered for specific type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>The resolved types.</returns>
		public static Array GetAllComponents(Type type)
		{
			return WindsorContainer.ResolveAll(type);
		}

		/// <summary>
		/// Registers all component instances of type in specific assembly.
		/// </summary>
		/// <param name="type">The type to register.</param>
		/// <param name="assembly">The assembly.</param>
		/// <param name="lifestyleType">Type of the lifestyle.</param>
		public static void RegisterAllComponentsOfType(
			Type type,
			Assembly assembly,
			LifestyleType lifestyleType = LifestyleType.Transient)
		{
			var typesToRegister = assembly.GetTypes().Where(type.IsAssignableFrom);

			foreach (var typeToRegister in typesToRegister)
			{
				WindsorContainer.Register(
					Component.For(typeToRegister).Named(typeToRegister.FullName)
					.LifeStyle.Is(lifestyleType));
			}
		}
	}
}