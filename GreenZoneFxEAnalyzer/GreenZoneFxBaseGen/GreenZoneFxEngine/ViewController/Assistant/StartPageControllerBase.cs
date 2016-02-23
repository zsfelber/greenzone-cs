using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Assistant
{
	public static class StartPageControllerProps
	{
		public const int PROPERTY_17_TEXTBOX1_ID = 17;
		public const int PROPERTY_18_RADIOBUTTON2_ID = 18;
		public const int PROPERTY_19_RADIOBUTTON1_ID = 19;
		public static bool RmiGetProperty(IStartPageController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_TEXTBOX1_ID:
					value = controller.TextBox1;
					return true;
				case PROPERTY_18_RADIOBUTTON2_ID:
					value = controller.RadioButton2;
					return true;
				case PROPERTY_19_RADIOBUTTON1_ID:
					value = controller.RadioButton1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IStartPageController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IStartPageController controller, GreenRmiObjectBuffer buffer)
		{
			controller.TextBox1 = (LabelledController) buffer.ChangedProps[StartPageControllerProps.PROPERTY_17_TEXTBOX1_ID];
			controller.RadioButton2 = (RadioButtonController) buffer.ChangedProps[StartPageControllerProps.PROPERTY_18_RADIOBUTTON2_ID];
			controller.RadioButton1 = (RadioButtonController) buffer.ChangedProps[StartPageControllerProps.PROPERTY_19_RADIOBUTTON1_ID];
		}

		public static void AddDependencies(IStartPageController controller)
		{
			controller.Dependencies.Add(controller.TextBox1);
			controller.Dependencies.Add(controller.RadioButton2);
			controller.Dependencies.Add(controller.RadioButton1);
		}

		public static void SerializationRead(IStartPageController controller, SerializationInfo info, StreamingContext context)
		{
			controller.TextBox1 = (LabelledController) info.GetValue("TextBox1", typeof(LabelledController));
			controller.RadioButton2 = (RadioButtonController) info.GetValue("RadioButton2", typeof(RadioButtonController));
			controller.RadioButton1 = (RadioButtonController) info.GetValue("RadioButton1", typeof(RadioButtonController));
		}

		public static void SerializationWrite(IStartPageController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("TextBox1", controller.TextBox1);
			info.AddValue("RadioButton2", controller.RadioButton2);
			info.AddValue("RadioButton1", controller.RadioButton1);
		}

	}
	public abstract class StartPageControllerBase : AssistantPageController, IStartPageController
	{

		bool ___initialized = false;


		public StartPageControllerBase(GreenRmiManager rmiManager, AssistantFormController assistant)
			: base(rmiManager, assistant)
		{
			___initialized = true;
			StartPageControllerProps.AddDependencies(this);
		}

		public StartPageControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			StartPageControllerProps.Initialize(this, buffer);
			___initialized = true;
			StartPageControllerProps.AddDependencies(this);
		}

		protected StartPageControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			StartPageControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			StartPageControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			StartPageControllerProps.SerializationWrite(this, info, context);
		}

		LabelledController textBox1;
		public LabelledController TextBox1
		{
			get {
				return textBox1;
			}
			set {
				if (!___initialized) {
					textBox1= value;
					changed[StartPageControllerProps.PROPERTY_17_TEXTBOX1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		RadioButtonController radioButton2;
		public RadioButtonController RadioButton2
		{
			get {
				return radioButton2;
			}
			set {
				if (!___initialized) {
					radioButton2= value;
					changed[StartPageControllerProps.PROPERTY_18_RADIOBUTTON2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		RadioButtonController radioButton1;
		public RadioButtonController RadioButton1
		{
			get {
				return radioButton1;
			}
			set {
				if (!___initialized) {
					radioButton1= value;
					changed[StartPageControllerProps.PROPERTY_19_RADIOBUTTON1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (StartPageControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!StartPageControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
