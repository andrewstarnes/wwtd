using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Horizon {
	[CustomEditor(typeof(HorizonMaster))]
	public class HorizonMasterEditor : Editor {

		string installPath;
		string inspectorGUIPath;

		bool drag = false;
		float mouseStartPos;
		float mouseDragPos;
		float specialSens = 1;
		float sensitivity = 1;
		//bool dragVector = false;
		int dragVector = 1;
		string valueToChange = "";
		Event e;

		HorizON_LayerProps hmlp;
		string activeCtrlID;

		bool drawInspector;
		float scrollBarWidth = 32;
		public HorizonMaster hm;

		void OnEnable(){
			hm = (HorizonMaster)target;
			if (hm != null) {
				hm.InitLayerProps();
				hm.CheckMaterials ();
				hm.UpdateHorizonMaster();
				hm.UpdateMaterials ();
				hm.ShowWireFrame(hm.showWireF);
				SceneView.RepaintAll ();
			}
			string scriptLocation = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
			installPath = scriptLocation.Replace ("/Sources/Scripts/Editor/HorizonMasterEditor.cs", "");
			inspectorGUIPath = installPath + "/Sources/Scripts/Editor/InspectorGUI";
		}

		public override void OnInspectorGUI(){

			hm = (HorizonMaster)target;
			e = Event.current;

			if (drag) {
				if (valueToChange != "") {
					specialSens = 1;
					if (e.shift){specialSens = 3;}
					if (e.control) { specialSens = 0.1f; }
					if (dragVector == 1) typeof(HorizonMaster).GetField (valueToChange).SetValue (target,(float)typeof(HorizonMaster).GetField (valueToChange).GetValue (target)-e.delta.y*specialSens*sensitivity);
					if (dragVector == 2) typeof(HorizonMaster).GetField (valueToChange).SetValue (target,(Vector4)typeof(HorizonMaster).GetField (valueToChange).GetValue (target)- new Vector4(e.delta.y*specialSens*sensitivity, e.delta.y*specialSens*sensitivity, 0,0));
					if (dragVector == 3) typeof(HorizonMaster).GetField (valueToChange).SetValue (target,(Vector4)typeof(HorizonMaster).GetField (valueToChange).GetValue (target)- new Vector4(0, 0, e.delta.y*specialSens*sensitivity,0));
					if (dragVector == 4) typeof(HorizonMaster).GetField (valueToChange).SetValue (target,(Vector4)typeof(HorizonMaster).GetField (valueToChange).GetValue (target)- new Vector4(0, 0, 0, e.delta.y*specialSens*sensitivity));
					if (dragVector == 5) typeof(HorizON_LayerProps).GetField (valueToChange).SetValue (hmlp,(Vector4)typeof(HorizON_LayerProps).GetField (valueToChange).GetValue (hmlp)- new Vector4(e.delta.y*specialSens*sensitivity, e.delta.y*specialSens*sensitivity, 0,0));
					if (dragVector == 6) typeof(HorizON_LayerProps).GetField (valueToChange).SetValue (hmlp,(Vector4)typeof(HorizON_LayerProps).GetField (valueToChange).GetValue (hmlp)- new Vector4(0, 0, e.delta.y*specialSens*sensitivity,0));
					if (dragVector == 7) typeof(HorizON_LayerProps).GetField (valueToChange).SetValue (hmlp,(Vector4)typeof(HorizON_LayerProps).GetField (valueToChange).GetValue (hmlp)- new Vector4(0, 0, 0, e.delta.y*specialSens*sensitivity));
					GUI.changed = true;
				}
			} 
			if (e.type == EventType.MouseUp || e.type == EventType.Ignore) {
				drag= false;
				valueToChange = "";
				Repaint();
			}


	//===== Variables =================================================================================================================================================================

			float showLabelWidth = 70;

			string showFeaturesLabel = "| Show";
			if (hm.showFeatures) showFeaturesLabel = "| Hide"; else showFeaturesLabel = "| Show";
			string showScalingLabel = "| Show";
			if (hm.showScaling) showScalingLabel = "| Hide"; else showScalingLabel = "| Show";
			string showMainSettingsLabel = "| Show";
			if (hm.showMainSettings) showMainSettingsLabel = "| Hide"; else showMainSettingsLabel = "| Show";
			string showDetailSettingsLabel = "| Show";
			if (hm.showDetailSettings) showDetailSettingsLabel = "| Hide"; else showDetailSettingsLabel = "| Show";
			string showWaterSettingsLabel = "| Show";
			if (hm.showWaterSettings) showWaterSettingsLabel = "| Hide"; else showWaterSettingsLabel = "| Show";
			string showFogSettingsLabel = "| Show";
			if (hm.showFogSettings) showFogSettingsLabel = "| Hide"; else showFogSettingsLabel = "| Show";
			string showSnowSettingsLabel = "| Show";
			if (hm.showSnowSettings) showSnowSettingsLabel = "| Hide"; else showSnowSettingsLabel = "| Show";
			string showIBLSettingsLabel = "| Show";
			if (hm.showIBLSettings) showIBLSettingsLabel = "| Hide"; else showIBLSettingsLabel = "| Show";
			string showDispSettingsLabel = "| Show";
			if (hm.showDispSettings) showDispSettingsLabel = "| Hide"; else showDispSettingsLabel = "| Show";
			string showCliffSettingsLabel = "| Show";
			if (hm.showCliffSettings) showCliffSettingsLabel = "| Hide"; else showCliffSettingsLabel = "| Show";
			string showToolsLabel = "| Show";
			if (hm.showTools) showToolsLabel = "| Hide"; else showToolsLabel = "| Show";


	//===== GUI Layout ======================================================================================================================================================================

			Rect bgRect = EditorGUILayout.GetControlRect ();
			bgRect = new Rect (bgRect.x+1, bgRect.y-18, Screen.width-40, bgRect.height+1);

			Texture2D bgTex;
			Texture2D logoTex = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/Horizon[ON]Inspector_Logo.png", typeof (Texture2D))as Texture2D;
			Texture2D color01 = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/images/Horizon[ON]Inspector_Color_01.png", typeof (Texture2D))as Texture2D;
			Texture2D color02 = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/images/Horizon[ON]Inspector_Color_02.png", typeof (Texture2D))as Texture2D;
			Texture2D color03 = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/images/Horizon[ON]Inspector_Color_03.png", typeof (Texture2D))as Texture2D;
			Texture2D color04 = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/images/Horizon[ON]Inspector_Color_04.png", typeof (Texture2D))as Texture2D;
			Texture2D color05 = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/images/Horizon[ON]Inspector_Color_05.png", typeof (Texture2D))as Texture2D;
			Texture2D color06 = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/images/Horizon[ON]Inspector_Color_06.png", typeof (Texture2D))as Texture2D;
			Texture2D color07 = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/images/Horizon[ON]Inspector_Color_07.png", typeof (Texture2D))as Texture2D;
			Texture2D color08 = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/images/Horizon[ON]Inspector_Color_08.png", typeof (Texture2D))as Texture2D;
			Texture2D color09 = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/images/Horizon[ON]Inspector_Color_09.png", typeof (Texture2D))as Texture2D;
			Texture2D color10 = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/images/Horizon[ON]Inspector_Color_10.png", typeof (Texture2D))as Texture2D;
			if (EditorGUIUtility.isProSkin) {
				bgTex = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/Horizon[ON]Inspector_bgTex_DarkSkin.jpg", typeof(Texture2D))as Texture2D;
			} else {
				bgTex = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/Horizon[ON]Inspector_bgTex_LightSkin.jpg", typeof(Texture2D))as Texture2D;
			}

			EditorGUI.DrawPreviewTexture (bgRect, bgTex);
			GUI.DrawTexture (new Rect ((Screen.width/2)-110,bgRect.y+7, 210,36), logoTex);

			EditorGUILayout.Space ();
			EditorGUILayout.Space ();

	//===== Features ======================================================================================================================================================================

			if (hm.setFeatures) {
				if (!hm.isPreset) {
					// Features Header
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Horizon[ON] Features", EditorStyles.boldLabel, GUILayout.Width (Screen.width - showLabelWidth - scrollBarWidth));
					EditorGUILayout.LabelField (showFeaturesLabel, GUILayout.Width (53));
					hm.showFeatures = EditorGUILayout.Foldout (hm.showFeatures, "");
					EditorGUILayout.EndHorizontal ();
					GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x, GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, Screen.width - 5 - scrollBarWidth, 2), color01);

					if (hm.showFeatures) {
						EditorGUILayout.GetControlRect (GUILayout.Height (0));
			
					// Features Content

						// LayerCount
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField ("Layercount", EditorStyles.label, GUILayout.Width (Screen.width - 108 - scrollBarWidth));
						hm.hFeatLayerCount = (HorizonMaster.LayerCount)EditorGUILayout.EnumPopup (hm.hFeatLayerCount, GUILayout.Width (100));
						EditorGUILayout.EndHorizontal ();

						// Diffuse Lighting Mode
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField ("Diffuse Lighting Mode", EditorStyles.label, GUILayout.Width (Screen.width - 108 - scrollBarWidth));
						hm.hFeatDiffLightMode = (HorizonMaster.DiffLightMode)EditorGUILayout.EnumPopup (hm.hFeatDiffLightMode, GUILayout.Width (100));
						EditorGUILayout.EndHorizontal ();
			
						// Direct Specularity
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField ("Enable Specularity", EditorStyles.label, GUILayout.Width (Screen.width - 107 - scrollBarWidth));
						hm.hFeatDirSpec = EditorGUILayout.Toggle (hm.hFeatDirSpec, GUILayout.Width (100));
						EditorGUILayout.EndHorizontal ();
				
						// Cubemap Reflection
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField ("Enable Reflections", EditorStyles.label, GUILayout.Width (Screen.width - 107 - scrollBarWidth));
						hm.hFeatCubeRefl = EditorGUILayout.Toggle (hm.hFeatCubeRefl, GUILayout.Width (100));
						EditorGUILayout.EndHorizontal ();

						// NormalMapping
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField ("Enable Normalmapping", EditorStyles.label, GUILayout.Width (Screen.width - 107 - scrollBarWidth));
						hm.hFeatNormalmaps = EditorGUILayout.Toggle (hm.hFeatNormalmaps, GUILayout.Width (100));
						EditorGUILayout.EndHorizontal ();

						// Emissiveness
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField ("Enable Emissiveness", EditorStyles.label, GUILayout.Width (Screen.width - 107 - scrollBarWidth));
						hm.hFeatEmissivness = EditorGUILayout.Toggle (hm.hFeatEmissivness, GUILayout.Width (100));
						EditorGUILayout.EndHorizontal ();

						// DetailTex
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField ("Enable Detail Textures", EditorStyles.label, GUILayout.Width (Screen.width - 107 - scrollBarWidth));
						hm.hFeatDetailTex = EditorGUILayout.Toggle (hm.hFeatDetailTex, GUILayout.Width (100));
						EditorGUILayout.EndHorizontal ();

						// Water
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField ("Enable Water", EditorStyles.label, GUILayout.Width (Screen.width - 107 - scrollBarWidth));
						hm.hFeatWater = EditorGUILayout.Toggle (hm.hFeatWater, GUILayout.Width (100));
						EditorGUILayout.EndHorizontal ();

						// Fog
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField ("Enable Fog", EditorStyles.label, GUILayout.Width (Screen.width - 107 - scrollBarWidth));
						hm.hFeatFog = EditorGUILayout.Toggle (hm.hFeatFog, GUILayout.Width (100));
						EditorGUILayout.EndHorizontal ();

						// Snow
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField ("Enable Snow", EditorStyles.label, GUILayout.Width (Screen.width - 107 - scrollBarWidth));
						hm.hFeatSnow = EditorGUILayout.Toggle (hm.hFeatSnow, GUILayout.Width (100));
						EditorGUILayout.EndHorizontal ();
					}
					GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x + (Screen.width - 5 - scrollBarWidth), GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, 1 - (Screen.width - 5 - scrollBarWidth), 2), color01);
					EditorGUILayout.Space ();
				}
			}


	//===== Scaling ======================================================================================================================================================================

			if (hm.childUsesTransition) {

				string tTipScaleInner = 
					"This controls how big the blending area is between the terrain and Horizon[ON].";
				string tTipScaleHeight = 
					"This controls how high the outer transition extends into the sky.";

				// Scaling Header
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("Transition Settings", EditorStyles.boldLabel, GUILayout.Width (Screen.width - showLabelWidth - scrollBarWidth));
				EditorGUILayout.LabelField (showScalingLabel, GUILayout.Width (53));
				hm.showScaling = EditorGUILayout.Foldout (hm.showScaling, "");
				EditorGUILayout.EndHorizontal ();
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x, GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, Screen.width - 5 - scrollBarWidth, 2), color02);

				if (hm.showScaling) {
					EditorGUILayout.GetControlRect (GUILayout.Height (0));

					// Scaling Content

					// Scale Inner
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Scale Inner", tTipScaleInner), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetScaleInner = EditorGUILayout.Slider (hm.hSetScaleInner, 0, 1, GUILayout.Width (129)); // Remember to fix slider range!
					EditorGUILayout.EndHorizontal ();

					// Scale Outer
					EditorGUILayout.BeginHorizontal ();
					ButtonDragSingle ("Scale Outer", "", "hSetScaleOuter", 0.01f);
					if (hm.hSetScaleOuter <= 0) hm.hSetScaleOuter = 0;
					GUILayout.Space (79);
					hm.hSetScaleOuter = EditorGUILayout.FloatField (hm.hSetScaleOuter, GUILayout.Width (50));
					EditorGUILayout.EndHorizontal ();

					// Scale Height
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Scale Height", tTipScaleHeight), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetScaleHeight = EditorGUILayout.Slider (hm.hSetScaleHeight, 0, 1, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal ();
				}
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x + (Screen.width - 5 - scrollBarWidth), GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, 1 - (Screen.width - 5 - scrollBarWidth), 2), color02);
				EditorGUILayout.Space ();
			}


	//===== Main Settings ======================================================================================================================================================================

			if (hm.horizonChildAvailable) {

				string tTipLockMask = 
					"If this is enabled, the textures of horizon will move with the object when you move it, otherwise they will stay in place. Locking is usefull if you want to move horizon with your camera, this will give the apearance of an infinite terrain. Locking may be usefull if you want to do worldshifting.";
				string tTipTexMask = 
					"This mask defines where the layers and or the water will be drawn. Layer 1 is drawn everywhere and the following layers will overdraw the previous ones. This mask can easily be painted in an image editor, you may take a screenshot from above as a guideline if you want to match certain layers with your terrain.";
				string tTipColTint = 
					"This is a global color tint, middle grey is neutral, you can use this if your horizon is too bright or too dark in general.";
				string tTipEmissColor = 
					"The emission color will be taken from the layer colormaps where the alpha of the layer is white. This color is usefull if you want to tint this colors. The alpha value is a multiplier for the intensity.";
				string tTipAmbOverride = 
					"You can use this color to override the ambient light for Horizon[ON]. How much of it is overridden is controlled by the alpha value.";
				string tTipAmbvsIBL = 
					"Here you can gradually fade between IBL and ambient light.";
				string tTipNMIntens = 
					"This controls globally how strong the normalmaps of the layers are applied.";

				// Main Settings Header
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("Main Settings", EditorStyles.boldLabel, GUILayout.Width (Screen.width - showLabelWidth - scrollBarWidth));
				EditorGUILayout.LabelField (showMainSettingsLabel, GUILayout.Width (53));
				hm.showMainSettings = EditorGUILayout.Foldout (hm.showMainSettings, "");
				EditorGUILayout.EndHorizontal ();
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x, GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, Screen.width - 5 - scrollBarWidth, 2), color03);

				if (hm.showMainSettings) {
					EditorGUILayout.GetControlRect (GUILayout.Height (0));
				
					// Main Settings Content

					// Mask Scale
					EditorGUILayout.BeginHorizontal ();
					ButtonDragSingle ("Scale", "(m)", "hSetMaskScaleOffset" ,1, 2, -1, "MaskScale");
					//EditorGUILayout.LabelField ("Scale (m)", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetMaskScaleOffset.x = EditorGUILayout.FloatField (hm.hSetMaskScaleOffset.x, GUILayout.Width (63));
					hm.hSetMaskScaleOffset.y = EditorGUILayout.FloatField (hm.hSetMaskScaleOffset.y, GUILayout.Width (62));
					EditorGUILayout.EndHorizontal ();

					// Mask Offset
					EditorGUILayout.BeginHorizontal ();
					ButtonDragDouble ("OffsetX", "OffsetY", "", "hSetMaskScaleOffset", "hSetMaskScaleOffset" ,1 , 3, 4, -1, "MaskOffset");
					//EditorGUILayout.LabelField ("Offset (m)", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetMaskScaleOffset.z = EditorGUILayout.FloatField (hm.hSetMaskScaleOffset.z, GUILayout.Width (63));
					hm.hSetMaskScaleOffset.w = EditorGUILayout.FloatField (hm.hSetMaskScaleOffset.w, GUILayout.Width (62));
					EditorGUILayout.EndHorizontal ();

					// Lock Mask
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("", EditorStyles.label, GUILayout.Width (Screen.width - 138 - scrollBarWidth));
					EditorGUILayout.LabelField (new GUIContent("Use local Space", tTipLockMask), EditorStyles.label, GUILayout.Width (111));
					hm.hSetLockMask = EditorGUILayout.Toggle (hm.hSetLockMask, GUILayout.Width (16));
					EditorGUILayout.EndHorizontal ();

					// Mask
					if (hm.hFeatLayerCount != HorizonMaster.LayerCount.One || hm.hFeatWater) {
						EditorGUILayout.LabelField (new GUIContent("Mask (RGBA)", tTipTexMask), EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						Rect rectA = GUILayoutUtility.GetRect (0, 0);
						rectA = new Rect (rectA.x + (Screen.width - 133 - scrollBarWidth), rectA.y - 16, 129, 129);
						hm.hTexMask = EditorGUI.ObjectField (rectA, hm.hTexMask, typeof(Texture), false) as Texture;
						GUILayout.Box ("Layers are drawn on\ntop of each other:\n\nLayer 1 is base...\nR = Layer 2\nG = Layer 3\nB = Layer 4\nA = Water", EditorStyles.miniLabel, GUILayout.Width (132), GUILayout.Height (112));
					}
					// ColorTint
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Global Tint", tTipColTint), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hColTint = EditorGUILayout.ColorField (hm.hColTint, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal ();

					// Emission Color
					if (hm.hFeatEmissivness) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Emission Color", tTipEmissColor), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hColEmissColor = EditorGUILayout.ColorField (hm.hColEmissColor, GUILayout.Width (129));
						EditorGUILayout.EndHorizontal ();
					}
					// AmbientOverride
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Ambient Override (A)Amount", tTipAmbOverride), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hColAmbOverride = EditorGUILayout.ColorField (hm.hColAmbOverride, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal ();

					// Ambient vs. IBL
					if (hm.hFeatDiffLightMode != HorizonMaster.DiffLightMode.Ambient) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Ambient vs. IBL", tTipAmbvsIBL), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetAmbvsIBL = EditorGUILayout.Slider (hm.hSetAmbvsIBL, 0, 1, GUILayout.Width (129));
						EditorGUILayout.EndHorizontal ();
					}
					// Normal Intensity
					if (hm.hFeatNormalmaps) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Normal Intensity", tTipNMIntens), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetNMIntensLayers = EditorGUILayout.Slider (hm.hSetNMIntensLayers, 0, 1, GUILayout.Width (129));
						EditorGUILayout.EndHorizontal ();
					}
				}
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x + (Screen.width - 5 - scrollBarWidth), GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, 1 - (Screen.width - 5 - scrollBarWidth), 2), color03);
				EditorGUILayout.Space ();
			}


	//===== Layer Settings ======================================================================================================================================================================

			if (hm.horizonChildAvailable) {

				string tTipLayerDiff = 
					"The colormap of this layer.";
				string tTipAIsGlossMask = 
					"If this is enabled the alpha channel of the layer colormap will be used as a gloss mask.";
				string tTipAIsEmissMask = 
					"If this is enabled the alpha channel of the layer colormap will be used as an emission mask.";
				string tTipLayerNM = 
					"The normalmap of this layer.";
				string tTipLayerTint = 
					"Set the color to tint this layer, middle grey is neutral. The alpha channel controls the saturation of this layer.";
				string tTipLayerSpecGloss = 
					"The specular color of this layer. The alpha channel controls the glossines.";
				string tTipLayerDetIntens = 
					"Controls how strong the detail textures are applied to this layer.";


				// Layer Header
				for (int i = 0; i < hm.layerCount; i++) {
					string showLayerSettingsLabel = "| Show";
					if (hm.hSetLayerProps [i].showLayerSettings)
						showLayerSettingsLabel = "| Hide";
					else
						showLayerSettingsLabel = "| Show";
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Layer " + (i + 1).ToString () + " Settings", EditorStyles.boldLabel, GUILayout.Width (Screen.width - showLabelWidth - scrollBarWidth));
					EditorGUILayout.LabelField (showLayerSettingsLabel, GUILayout.Width (53));
					hm.hSetLayerProps [i].showLayerSettings = EditorGUILayout.Foldout (hm.hSetLayerProps [i].showLayerSettings, "");
					EditorGUILayout.EndHorizontal ();
					GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x, GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, Screen.width - 5 - scrollBarWidth, 2), color04);

					if (hm.hSetLayerProps [i].showLayerSettings) {
						EditorGUILayout.GetControlRect (GUILayout.Height (0));
				
						// Layer Settings Content

						// Diffuse Texture
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Colormap", tTipLayerDiff), EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						Rect rectA = GUILayoutUtility.GetRect (0, 0);
						rectA = new Rect (rectA.x, rectA.y, 129, 129);
						hm.hSetLayerProps [i].hTexLayerDiff = EditorGUI.ObjectField (rectA, hm.hSetLayerProps [i].hTexLayerDiff, typeof(Texture), false) as Texture;
						EditorGUILayout.EndHorizontal ();



						// A is Gloss
						if (hm.hFeatDirSpec || hm.hFeatCubeRefl) {
							EditorGUILayout.BeginHorizontal ();
							hm.hSetLayerProps [i].hSetAIsGlossMask = EditorGUILayout.Toggle (hm.hSetLayerProps [i].hSetAIsGlossMask, GUILayout.Width (12));
							EditorGUILayout.LabelField (new GUIContent("A = Gloss", tTipAIsGlossMask), EditorStyles.label, GUILayout.Width (85));
							EditorGUILayout.EndHorizontal ();
						} else {
							GUILayout.Box ("", EditorStyles.label, GUILayout.Width (106));
						}

						// A is Emission
						if (hm.hFeatEmissivness) {
							EditorGUILayout.BeginHorizontal ();
							hm.hSetLayerProps [i].hSetAIsEmissMask = EditorGUILayout.Toggle (hm.hSetLayerProps [i].hSetAIsEmissMask, GUILayout.Width (12));
							EditorGUILayout.LabelField (new GUIContent("A = Emission", tTipAIsEmissMask), EditorStyles.label, GUILayout.Width (85));
							EditorGUILayout.EndHorizontal ();
						} else {
							GUILayout.Box ("", EditorStyles.label, GUILayout.Width (106));
						}

						EditorGUILayout.GetControlRect (GUILayout.Height (76));

						// Normal Texture
						if (hm.hFeatNormalmaps) {
							EditorGUILayout.BeginHorizontal ();
							EditorGUILayout.LabelField (new GUIContent("Normalmap", tTipLayerNM), EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
							Rect rectB = EditorGUILayout.GetControlRect ();
							rectB = new Rect (rectB.x, rectB.y, 128, 16);
							hm.hSetLayerProps [i].hTexLayerNM = EditorGUI.ObjectField (rectB, hm.hSetLayerProps [i].hTexLayerNM, typeof(Texture), false) as Texture;
							EditorGUILayout.EndHorizontal ();
						}

						// Map Scale
						EditorGUILayout.BeginHorizontal ();
						ButtonDragSingle ("Tiling","" , "hSetLayerScaleOffset", 0.001f, 5, i, "layerTiling"+i.ToString());
						//EditorGUILayout.LabelField ("Tiling", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetLayerProps [i].hSetLayerScaleOffset.x = EditorGUILayout.FloatField (hm.hSetLayerProps [i].hSetLayerScaleOffset.x, GUILayout.Width (63));
						hm.hSetLayerProps [i].hSetLayerScaleOffset.y = EditorGUILayout.FloatField (hm.hSetLayerProps [i].hSetLayerScaleOffset.y, GUILayout.Width (62));
						EditorGUILayout.EndHorizontal ();
					
						// Map Offset
						EditorGUILayout.BeginHorizontal ();
						ButtonDragDouble ("OffsetX","OffsetY", "", "hSetLayerScaleOffset", "hSetLayerScaleOffset", 0.001f, 6, 7, i, "layerOffset"+i.ToString());
						//EditorGUILayout.LabelField ("Offset", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetLayerProps [i].hSetLayerScaleOffset.z = EditorGUILayout.FloatField (hm.hSetLayerProps [i].hSetLayerScaleOffset.z, GUILayout.Width (63));
						hm.hSetLayerProps [i].hSetLayerScaleOffset.w = EditorGUILayout.FloatField (hm.hSetLayerProps [i].hSetLayerScaleOffset.w, GUILayout.Width (62));
						EditorGUILayout.EndHorizontal ();

						// Tint
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Tint / Saturation", tTipLayerTint), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetLayerProps [i].hColLayerTint = EditorGUILayout.ColorField (hm.hSetLayerProps [i].hColLayerTint, GUILayout.Width (129));
						EditorGUILayout.EndHorizontal ();

						// Layer Spec Gloss
						if (hm.hFeatDirSpec || hm.hFeatCubeRefl) {
							EditorGUILayout.BeginHorizontal ();
							EditorGUILayout.LabelField (new GUIContent("Spec / Gloss", tTipLayerSpecGloss), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
							hm.hSetLayerProps [i].hColLayerSpecGloss = EditorGUILayout.ColorField (hm.hSetLayerProps [i].hColLayerSpecGloss, GUILayout.Width (129));
							EditorGUILayout.EndHorizontal ();
						}
						// Layer Detail Intensity
						if (hm.hFeatDetailTex) {
							EditorGUILayout.BeginHorizontal ();
							EditorGUILayout.LabelField (new GUIContent("Detail Intensity", tTipLayerDetIntens), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
							hm.hSetLayerProps [i].hSetLayerDetIntens = EditorGUILayout.Slider (hm.hSetLayerProps [i].hSetLayerDetIntens, 0, 1, GUILayout.Width (129));
							EditorGUILayout.EndHorizontal ();
						}
					}
					GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x + (Screen.width - 5 - scrollBarWidth), GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, 1 - (Screen.width - 5 - scrollBarWidth), 2), color04);
					EditorGUILayout.Space ();
				}
			}

	//===== Detail Settings ======================================================================================================================================================================

			if (hm.hFeatDetailTex && hm.horizonChildAvailable) {

				string tTipetailTexDiff = 
					"The detail colormap. It is blended in overlay mode, that means that pixels darker than 128 will darken and pixels brighter than 128 will brighten the underlying layers.";
				string tTipDetailTexNM = 
					"The detail normalmap.";
				string tTipDetailDiffIntens = 
					"Controls how strong the detail colormap will be blended.";
				string tTipDetailNMIntens = 
					"Controls how strong the detail normalmap will be blended.";

			// Detail Settings Header
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("Detail Settings", EditorStyles.boldLabel, GUILayout.Width (Screen.width - showLabelWidth - scrollBarWidth));
				EditorGUILayout.LabelField (showDetailSettingsLabel, GUILayout.Width (53));
				hm.showDetailSettings = EditorGUILayout.Foldout (hm.showDetailSettings, "");
				EditorGUILayout.EndHorizontal ();
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x, GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, Screen.width - 5 - scrollBarWidth, 2), color05);
			
				if (hm.showDetailSettings) {
					EditorGUILayout.GetControlRect (GUILayout.Height (0));
				
				// Detail Settings Content
				
					// Scale
					EditorGUILayout.BeginHorizontal ();
					ButtonDragSingle ("Tiling","" , "hSetDetailScaleOffset", 0.05f, 2, -1, "DetailScale");
					hm.hSetDetailScaleOffset.x = EditorGUILayout.FloatField (hm.hSetDetailScaleOffset.x, GUILayout.Width (63));
					hm.hSetDetailScaleOffset.y = EditorGUILayout.FloatField (hm.hSetDetailScaleOffset.y, GUILayout.Width (62));
					EditorGUILayout.EndHorizontal ();
				
					// Offset
					EditorGUILayout.BeginHorizontal ();
					ButtonDragDouble ("OffsetX", "OffsetY", "", "hSetDetailScaleOffset", "hSetDetailScaleOffset", 0.001f, 3, 4, -1, "DetailOffset");
					//EditorGUILayout.LabelField ("Offset", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetDetailScaleOffset.z = EditorGUILayout.FloatField (hm.hSetDetailScaleOffset.z, GUILayout.Width (63));
					hm.hSetDetailScaleOffset.w = EditorGUILayout.FloatField (hm.hSetDetailScaleOffset.w, GUILayout.Width (62));
					EditorGUILayout.EndHorizontal ();
				
					// ColorMap
					EditorGUILayout.LabelField (new GUIContent("Colormap", tTipetailTexDiff), EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					Rect rectA = GUILayoutUtility.GetRect (0, 0);
					rectA = new Rect (rectA.x + (Screen.width - 133 - scrollBarWidth), rectA.y - 16, 129, 129);
					hm.hTexDetailTexDiff = EditorGUI.ObjectField (rectA, hm.hTexDetailTexDiff, typeof(Texture), false) as Texture;
					GUILayout.Box ("Colormap is blended\nin overlay mode...\n\nEverything below\nmiddle grey is\ndarkening and\neverything above\nmiddle grey is\nbrightening.", EditorStyles.miniLabel, GUILayout.Width (132), GUILayout.Height (112));

					// Normal Texture
					if (hm.hFeatNormalmaps) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Normalmap", tTipDetailTexNM), EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						Rect rectB = EditorGUILayout.GetControlRect ();
						rectB = new Rect (rectB.x, rectB.y, 128, 16);
						hm.hTexDetailTexNM = EditorGUI.ObjectField (rectB, hm.hTexDetailTexNM, typeof(Texture), false) as Texture;
						EditorGUILayout.EndHorizontal ();
					}			
					// Colormap Intensity
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Color Intensity", tTipDetailDiffIntens), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetDetailDiffIntens = EditorGUILayout.Slider (hm.hSetDetailDiffIntens, 0, 1, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal ();

					// Normal Intensity
					if (hm.hFeatNormalmaps) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Normal Intensity", tTipDetailNMIntens), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetDetailNMIntens = EditorGUILayout.Slider (hm.hSetDetailNMIntens, 0, 1, GUILayout.Width (129));
						EditorGUILayout.EndHorizontal ();
					}


				}
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x + (Screen.width - 5 - scrollBarWidth), GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, 1 - (Screen.width - 5 - scrollBarWidth), 2), color05);
				EditorGUILayout.Space ();
			}


	//===== Water Settings ======================================================================================================================================================================
			
			if (hm.hFeatWater && hm.horizonChildAvailable) {

				string tTipWaterColorOpac = 
					"The diffuse color of the water. The alpha channel controls the opacity of the water.";
				string tTiphWaterSpecGloss = 
					"The specular color of the water. The alpha channel controls the glossines of the water";
				string tTipWaterBlend = 
					"Controls the visibility of the water, can be used to simulate partially wet areas.";
				string tTipWaterNM = 
					"The normalmap for the water.";
				string tTipWaterWavesIntens = 
					"Controls the intensity of the waves.";

			// Water Settings Header
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("Water Settings", EditorStyles.boldLabel, GUILayout.Width (Screen.width - showLabelWidth - scrollBarWidth));
				EditorGUILayout.LabelField (showWaterSettingsLabel, GUILayout.Width (53));
				hm.showWaterSettings = EditorGUILayout.Foldout (hm.showWaterSettings, "");
				EditorGUILayout.EndHorizontal ();
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x, GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, Screen.width - 5 - scrollBarWidth, 2), color06);
				
				if (hm.showWaterSettings) {
					EditorGUILayout.GetControlRect (GUILayout.Height (0));
					
				// Water Settings Content

					// Color
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField (new GUIContent("Color / Opacity", tTipWaterColorOpac), EditorStyles.label, GUILayout.Width (Screen.width-137-scrollBarWidth));
					hm.hColWaterColorOpac = EditorGUILayout.ColorField (hm.hColWaterColorOpac, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal();

					// Spec Gloss
					if (hm.hFeatDirSpec || hm.hFeatCubeRefl){
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField (new GUIContent("Spec / Gloss", tTiphWaterSpecGloss), EditorStyles.label, GUILayout.Width (Screen.width-137-scrollBarWidth));
						hm.hColWaterSpecGloss = EditorGUILayout.ColorField (hm.hColWaterSpecGloss, GUILayout.Width (129));
						EditorGUILayout.EndHorizontal();
					}

					// Blend
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Water Blend", tTipWaterBlend), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetWaterBlend = EditorGUILayout.Slider (hm.hSetWaterBlend, 0, 1, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal ();
															
					// NormalMap
					if (hm.hFeatNormalmaps) {
						EditorGUILayout.LabelField (new GUIContent("Normalmap", tTipWaterNM), EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						Rect rectA = GUILayoutUtility.GetRect (0, 0);
						rectA = new Rect (rectA.x + (Screen.width - 133 - scrollBarWidth), rectA.y - 16, 129, 129);
						hm.hTexWaterNM = EditorGUI.ObjectField (rectA, hm.hTexWaterNM, typeof(Texture), false) as Texture;
						//GUILayout.Box ("Colormap is blended\nin overlay mode...\n\nEverything below\nmiddle grey is\ndarkening and\neverything above\nmiddle grey is\nbrightening.", EditorStyles.miniLabel, GUILayout.Width (132), GUILayout.Height (112));
						EditorGUILayout.GetControlRect(GUILayout.Height(112));

						// Scale
						EditorGUILayout.BeginHorizontal ();
						ButtonDragSingle ("Tiling","" , "hSetWaterScaleOffset", 0.05f, 2);
						//EditorGUILayout.LabelField ("Tiling", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetWaterScaleOffset.x = EditorGUILayout.FloatField (hm.hSetWaterScaleOffset.x, GUILayout.Width (63));
						hm.hSetWaterScaleOffset.y = EditorGUILayout.FloatField (hm.hSetWaterScaleOffset.y, GUILayout.Width (62));
						EditorGUILayout.EndHorizontal ();
																	
						// Normal Intensity
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Waviness", tTipWaterWavesIntens), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetWaterWavesIntens = EditorGUILayout.Slider (hm.hSetWaterWavesIntens, 0, 1, GUILayout.Width (129));
						EditorGUILayout.EndHorizontal ();

						// Wave Speed
						EditorGUILayout.BeginHorizontal ();
						ButtonDragSingle ("Wave Speed","" , "HSetWaterWavesSpeed", 0.001f, 1);
						//EditorGUILayout.LabelField ("Wave Speed", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.HSetWaterWavesSpeed = EditorGUILayout.FloatField (hm.HSetWaterWavesSpeed, GUILayout.Width (130));
						EditorGUILayout.EndHorizontal ();
					}
					
					
				}
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x + (Screen.width - 5 - scrollBarWidth), GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, 1 - (Screen.width - 5 - scrollBarWidth), 2), color06);
				EditorGUILayout.Space ();
			}


	//===== Fog Settings ======================================================================================================================================================================
			
			if (hm.hFeatFog && hm.horizonChildAvailable) {

				string tTipFogIntens = 
					"This is the global setting for the amount of fog.";
				string tTiphColFogColorAmbBlend = 
					"The color of the fog, should be similiar to the ambient light color. The alpha channel controls how much of the color should be taken from the ambient light color.";
				string tTipFogSpecCubeAdd = 
					"Controls how much of the reflection cubemap color should be added to the fog color. This can give some directional dependence to the fog and might also help to match the fog color with the sky.";
				string tTipFogEmissPunchThru = 
					"Controls how strong the emissive color will shine through the fog.";

			// Fog Settings Header
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("Fog Settings", EditorStyles.boldLabel, GUILayout.Width (Screen.width - showLabelWidth - scrollBarWidth));
				EditorGUILayout.LabelField (showFogSettingsLabel, GUILayout.Width (53));
				hm.showFogSettings = EditorGUILayout.Foldout (hm.showFogSettings, "");
				EditorGUILayout.EndHorizontal ();
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x, GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, Screen.width - 5 - scrollBarWidth, 2), color07);
				
				if (hm.showFogSettings) {
					EditorGUILayout.GetControlRect (GUILayout.Height (0));
					
				// Fog Settings Content
					
					// Fog Amount
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField (new GUIContent("Fog Amount", tTipFogIntens), EditorStyles.label, GUILayout.Width (Screen.width-137-scrollBarWidth));
					hm.hSetFogIntens = EditorGUILayout.Slider (hm.hSetFogIntens, 0, 1, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal();
					
					// Color
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField (new GUIContent("Color/Amb. Blend", tTiphColFogColorAmbBlend), EditorStyles.label, GUILayout.Width (Screen.width-137-scrollBarWidth));
					hm.hColFogColorAmbBlend = EditorGUILayout.ColorField (hm.hColFogColorAmbBlend, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal();
					
					// Cube Add
					if (hm.hFeatCubeRefl){
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Cubemap Add", tTipFogSpecCubeAdd), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetFogSpecCubeAdd = EditorGUILayout.Slider (hm.hSetFogSpecCubeAdd, 0, 1, GUILayout.Width (129));
						EditorGUILayout.EndHorizontal ();
					}
					// Start Distance
					EditorGUILayout.BeginHorizontal ();
					ButtonDragDouble ("Start","End","(m)", "hSetFogStartDist", "hSetFogTransDist");
					hm.hSetFogStartDist = EditorGUILayout.FloatField (hm.hSetFogStartDist, GUILayout.Width (63));
					hm.hSetFogTransDist = EditorGUILayout.FloatField (hm.hSetFogTransDist, GUILayout.Width (62));
					EditorGUILayout.EndHorizontal ();

					// Start Height
					EditorGUILayout.BeginHorizontal ();
					ButtonDragDouble ("Base","Height","(m)", "hSetFogStartHeight", "hSetFogTransHeight");
					hm.hSetFogStartHeight = EditorGUILayout.FloatField (hm.hSetFogStartHeight, GUILayout.Width (63));
					hm.hSetFogTransHeight = EditorGUILayout.FloatField (hm.hSetFogTransHeight, GUILayout.Width (62));
					EditorGUILayout.EndHorizontal ();

					// Height offset by distance
					EditorGUILayout.BeginHorizontal ();
					ButtonDragSingle ("Height Offset by Distance","" , "hSetFogDistHeightOffset", 1f, 1);
					GUILayout.Space(67);
					//EditorGUILayout.LabelField ("Height Offset by Distance (m)", EditorStyles.label, GUILayout.Width (Screen.width - 70 - scrollBarWidth));
					hm.hSetFogDistHeightOffset = EditorGUILayout.FloatField (hm.hSetFogDistHeightOffset, GUILayout.Width (62));
					EditorGUILayout.EndHorizontal ();

					// Emission punch through
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Emission seethru", tTipFogEmissPunchThru), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetFogEmissPunchThru = EditorGUILayout.Slider (hm.hSetFogEmissPunchThru, 0, 1, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal ();	
				}
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x + (Screen.width - 5 - scrollBarWidth), GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, 1 - (Screen.width - 5 - scrollBarWidth), 2), color07);
				EditorGUILayout.Space ();
			}


	//===== Snow Settings ======================================================================================================================================================================

			if (hm.hFeatSnow && hm.horizonChildAvailable) {

				string tTipSnowAmount = 
					"This is the global setting for the amount of snow.";
				string tTipSnowDiffColor = 
					"The color of the snow. A slight blueish tint is recommended";
				string tTipSnowSpecGloss = 
					"The specular color of the snow. The alpha value controls the glossiness of the snow, for ice a relatively high value is recommended.";
				string tTipSnowSlopeDamp = 
					"How steep/flat should a surface be so snow can lay on it";
				string tTipSnowReduceByColor =
					"This reduces the snow amount if the underlying color is dark. (Dark surfaces absorb more heat and therefor snow will need longer to gather)";

			// Snow Settings Header
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("Snow Settings", EditorStyles.boldLabel, GUILayout.Width (Screen.width - showLabelWidth - scrollBarWidth));
				EditorGUILayout.LabelField (showSnowSettingsLabel, GUILayout.Width (53));
				hm.showSnowSettings = EditorGUILayout.Foldout (hm.showSnowSettings, "");
				EditorGUILayout.EndHorizontal ();
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x, GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, Screen.width - 5 - scrollBarWidth, 2), color09);
				
				if (hm.showSnowSettings) {
					EditorGUILayout.GetControlRect (GUILayout.Height (0));
					
					// Snow Settings Content
					
					// Snow Amount
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(new GUIContent("Snow Amount", tTipSnowAmount), EditorStyles.label, GUILayout.Width (Screen.width-137-scrollBarWidth));
					hm.hsetSnowAmount = EditorGUILayout.Slider (hm.hsetSnowAmount, 0, 1, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal();
					
					// Color
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(new GUIContent("Snow Color", tTipSnowDiffColor), EditorStyles.label, GUILayout.Width (Screen.width-137-scrollBarWidth));
					hm.hColSnowDiffColor = EditorGUILayout.ColorField (hm.hColSnowDiffColor, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal();
					
					// Spec Gloss
					if (hm.hFeatCubeRefl || hm.hFeatDirSpec){
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Spec / Gloss", tTipSnowSpecGloss), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hColSnowSpecGloss = EditorGUILayout.ColorField (hm.hColSnowSpecGloss, GUILayout.Width (129));
						EditorGUILayout.EndHorizontal ();
					}
					// Height
					EditorGUILayout.BeginHorizontal ();
					ButtonDragDouble("Base", "Height", "(m)", "hSetSnowStartHeight", "hSetSnowHeightTrans");
					hm.hSetSnowStartHeight = EditorGUILayout.FloatField (hm.hSetSnowStartHeight, GUILayout.Width (63));
					hm.hSetSnowHeightTrans = EditorGUILayout.FloatField (hm.hSetSnowHeightTrans, GUILayout.Width (62));
					EditorGUILayout.EndHorizontal ();
					
					// Slope Damp
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Slope Damping", tTipSnowSlopeDamp),EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetSnowSlopeDamp = EditorGUILayout.Slider (hm.hSetSnowSlopeDamp, 0, 1, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal ();

					// Slope Damp
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Color Damping", tTipSnowReduceByColor),EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetSnowReduceByColor = EditorGUILayout.Slider (hm.hSetSnowReduceByColor, 0, 1, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal ();	
				}
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x + (Screen.width - 5 - scrollBarWidth), GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, 1 - (Screen.width - 5 - scrollBarWidth), 2), color09);
				EditorGUILayout.Space ();
			}


	//===== IBL Settings ======================================================================================================================================================================

			if (((hm.hFeatDiffLightMode != HorizonMaster.DiffLightMode.Ambient) || hm.hFeatCubeRefl) && hm.horizonChildAvailable) {

				// Tooltips
				string tTipCubeIBLDiff = 
					"The HDR cubemap for diffuse ambient lighting. The exposure value below controls is basically a multiplier for the diffuse image based lighting.";
				//string tTipCubeIBLSpec = 
				//	"The HDR cubemap for specular lighting, The MIP-levels in the input fields below control how blurry the reflections are at the minimum and maximum gloss setting. The Multiplier input fields below control how bright or dark the reflections are at the minimum and maximum gloss setting. (Gloss is set per layer in the layer foldouts)";

			// IBL Settings Header
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("IBL Settings", EditorStyles.boldLabel, GUILayout.Width (Screen.width - showLabelWidth - scrollBarWidth));
				EditorGUILayout.LabelField (showIBLSettingsLabel, GUILayout.Width (53));
				hm.showIBLSettings = EditorGUILayout.Foldout (hm.showIBLSettings, "");
				EditorGUILayout.EndHorizontal ();
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x, GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, Screen.width - 5 - scrollBarWidth, 2), color10);
			
				if (hm.showIBLSettings) {
					//EditorGUILayout.GetControlRect (GUILayout.Height (0));
				
					// IBL Settings Content
				
					// Diffuse Cubemap
					if (hm.hFeatDiffLightMode == HorizonMaster.DiffLightMode.CubeM) {
						EditorGUILayout.LabelField (new GUIContent("Diffuse\nCubemap", tTipCubeIBLDiff), EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth), GUILayout.Height (32));
						Rect rectA = GUILayoutUtility.GetRect (0, 0);
						rectA = new Rect (rectA.x + (Screen.width - 133 - scrollBarWidth), rectA.y - 32, 129, 129);
						hm.hCubeIBLDiff = EditorGUI.ObjectField (rectA, hm.hCubeIBLDiff, typeof(Cubemap), false) as Cubemap;
						GUILayout.Box ("", EditorStyles.miniLabel, GUILayout.Width (132), GUILayout.Height (96));
					}
					// Diffuse IBL Exposure
					if (hm.hFeatDiffLightMode != HorizonMaster.DiffLightMode.Ambient) {
						EditorGUILayout.BeginHorizontal ();
						ButtonDragSingle ("Exposure","" , "hSetIBLExposure", 0.001f, 1);
						//EditorGUILayout.LabelField ("Exposure", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetIBLExposure = EditorGUILayout.FloatField (hm.hSetIBLExposure, GUILayout.Width (129));
						EditorGUILayout.EndHorizontal ();
					}
					// Spec Cubemap
					if (hm.hFeatCubeRefl) {
						//EditorGUILayout.LabelField (new GUIContent("Reflection\nCubemap", tTipCubeIBLSpec), EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth), GUILayout.Height (32));
						//Rect rectA = GUILayoutUtility.GetRect (0, 0);
						//rectA = new Rect (rectA.x + (Screen.width - 133 - scrollBarWidth), rectA.y - 32, 129, 129);
						//hm.hCubeIBLSpec = EditorGUI.ObjectField (rectA, hm.hCubeIBLSpec, typeof(Cubemap), false) as Cubemap;
						//GUILayout.Box ("", EditorStyles.miniLabel, GUILayout.Width (132), GUILayout.Height (96));

						EditorGUILayout.BeginHorizontal ();
						ButtonDragDouble ("Rough MIP", "Multi", "", "hSetSpecRoughMIPLevel", "hSetSpecRoughMulti", 0.005f, 1, 1);
						//EditorGUILayout.LabelField ("Rough MIP/Multi", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetSpecRoughMIPLevel = EditorGUILayout.FloatField (hm.hSetSpecRoughMIPLevel, GUILayout.Width (63));
						if (hm.hSetSpecRoughMIPLevel <= 0) hm.hSetSpecRoughMIPLevel = 0;
						if (hm.hSetSpecRoughMIPLevel >= 8) hm.hSetSpecRoughMIPLevel = 8;
						hm.hSetSpecRoughMulti = EditorGUILayout.FloatField (hm.hSetSpecRoughMulti, GUILayout.Width (62));
						if (hm.hSetSpecRoughMulti <= 0) hm.hSetSpecRoughMulti = 0;
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						ButtonDragDouble ("Gloss MIP", "Multi", "", "hSetSpecGlossMIPLevel", "hSetSpecGlossMulti", 0.005f, 1, 1);
						//EditorGUILayout.LabelField ("Gloss MIP/Multi", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetSpecGlossMIPLevel = EditorGUILayout.FloatField (hm.hSetSpecGlossMIPLevel, GUILayout.Width (63));
						if (hm.hSetSpecGlossMIPLevel <= 0) hm.hSetSpecGlossMIPLevel = 0;
						if (hm.hSetSpecGlossMIPLevel >= 8) hm.hSetSpecGlossMIPLevel = 8;
						hm.hSetSpecGlossMulti = EditorGUILayout.FloatField (hm.hSetSpecGlossMulti, GUILayout.Width (62));
						if (hm.hSetSpecGlossMulti <= 0) hm.hSetSpecGlossMulti = 0;
						EditorGUILayout.EndHorizontal ();
					}

				}
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x + (Screen.width - 5 - scrollBarWidth), GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, 1 - (Screen.width - 5 - scrollBarWidth), 2), color10);
				EditorGUILayout.Space ();
			}


	//===== Displacement & Tesselation Settings ======================================================================================================================================================================

			if (hm.childUsesDisplacement) {

				// Tooltips
				string tTipDispHeightmap = 
					"The The heightmap for displacent. It uses 4 channels, ARGB, that means that the alpha channel is used for layer 1, the red channel is used for layer 2, the green channel is used for layer 3 and the blue channel is used for layer 4. Water (if enabled) will be flattened automatically.";
				string tTipDispRedByUV = 
					"When this is enabled, displacement will be reduced near the borders of the meshes. The mesh UVs are used for this. If you want to use your own meshes you can set the UVs in a way to reduce displacement where you want. A value of 0.5 will not reduce displacement but a value of 0 will and a vale of 1 will too. (UVs are not used for texturing)";
				string tTipDispRedByVertCol = 
					"When this is enabled you can use vertex painitng to reduce displacement.";
				string tTipDispRedFadeAmount = 
					"This slider controlls how strong the displacement will be reduced by the UV borders.";
				string tTipTessSubD =
					"This controls how often the mesh is subdivided";

				// Displacement Settings Header
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("Displacement Settings", EditorStyles.boldLabel, GUILayout.Width (Screen.width - showLabelWidth - scrollBarWidth));
				EditorGUILayout.LabelField (showDispSettingsLabel, GUILayout.Width (53));
				hm.showDispSettings = EditorGUILayout.Foldout (hm.showDispSettings, "");
				EditorGUILayout.EndHorizontal ();
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x, GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, Screen.width - 5 - scrollBarWidth, 2), color08);
			
				if (hm.showDispSettings) {
					//EditorGUILayout.GetControlRect (GUILayout.Height (0));
				
					// Displacement Settings Content

					// HeightMap
					EditorGUILayout.LabelField (new GUIContent("Heightmap", tTipDispHeightmap),EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					Rect rectA = GUILayoutUtility.GetRect (0, 0);
					rectA = new Rect (rectA.x + (Screen.width - 133 - scrollBarWidth), rectA.y - 16, 129, 129);
					hm.hTexDispHeightmap = EditorGUI.ObjectField (rectA, hm.hTexDispHeightmap, typeof(Texture), false) as Texture;
					GUILayout.Box ("Heightmap uses 4\nChannels (ARGB)...\nA = Layer 1\nR = Layer 2\nG = Layer 3\nB = Layer 4\n\nWater flattens\nautomatically", EditorStyles.miniLabel, GUILayout.Width (132), GUILayout.Height (112));

					// Disp Height
					EditorGUILayout.BeginHorizontal ();
					ButtonDragSingle("Height", "(m)", "hSetDispHeight");
					//EditorGUILayout.LabelField ("Height (m)", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetDispHeight = EditorGUILayout.FloatField (hm.hSetDispHeight, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal ();

					// Reduce by Border
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Flatten by UVs (near 0 and 1)", tTipDispRedByUV), EditorStyles.label, GUILayout.Width (Screen.width - 22 - scrollBarWidth));
					hm.hSetDispRedByUV = EditorGUILayout.Toggle (hm.hSetDispRedByUV, GUILayout.Width (160));
					EditorGUILayout.EndHorizontal ();

					// Reduce by Vert Color
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Flatten by Vertex Color (A)", tTipDispRedByVertCol), EditorStyles.label, GUILayout.Width (Screen.width - 22 - scrollBarWidth));
					hm.hSetDispRedByVertCol = EditorGUILayout.Toggle (hm.hSetDispRedByVertCol, GUILayout.Width (16));
					EditorGUILayout.EndHorizontal ();
				
					// Flatten Strength
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Flatten Strength", tTipDispRedFadeAmount), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetDispRedFadeAmount = EditorGUILayout.Slider (hm.hSetDispRedFadeAmount, 0.05f, 1, GUILayout.Width (129)); 
					EditorGUILayout.EndHorizontal ();

					// DX11 SubDs
					if (hm.childUsesTesselation){
						if (PlayerSettings.useDirect3D11) {
							EditorGUILayout.BeginHorizontal ();
							EditorGUILayout.LabelField (new GUIContent("SubDivisions", tTipTessSubD), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
							hm.hSetTessSubD = EditorGUILayout.Slider (hm.hSetTessSubD, 1, 40, GUILayout.Width (129));
							EditorGUILayout.EndHorizontal ();
						}
					}
				}
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x + (Screen.width - 5 - scrollBarWidth), GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, 1 - (Screen.width - 5 - scrollBarWidth), 2), color08);
				EditorGUILayout.Space ();
			}

	//===== Cliff Settings ======================================================================================================================================================================

			if (hm.childUsesCliffs) {

				// Tooltips
				string tTipCliffMainColormap = 
					"The main colormap for the cliff layer, this map is projected 2-planar in worldspace. \n(custom cliff meshes dont need UVs)";
				string tTipCliffAIsGlossMask = 
					"If this is enabled the alpha channel of the cliff colormap will be used as a glossmask.";
				string tTipCliffAIsEmissMask = 
					"If this is enabled the alpha channel of the cliff colormap will be used as emission mask. \n(Emission is controlled globally in the main settings foldout)";
				string tTipCliffMainNormalmap = 
					"The main normalmap for the cliff layer, this map is projected 2-planar in worldspace. \n(Custom cliff meshes dont need UVs)";
				string tTipCliffSpecGloss =
					"This controls the specular color of the cliff. \nIf \"A = Glossmask\" is disabled, gloss can be adjusted with the alpha value.";
				string tTipCliffDetailMode = 
					"Here you can choose if you want to use detail maps on the cliff. Detail normalmapping is not availible when the ambient lightmode is set to cubemap.";
				string tTipCliffDiffIntens = 
					"This controls the intensity of the detail colormap.";
				string tTipCliffNMIntens = 
					"This controls the intensity of the detail normalmap.";
				string tTipCliffDetDiff = 
					"The detail colormap for the cliff.";
				string tTipCliffDetNM = 
					"The detail normalmap for the cliff.";

				// Cliff Header
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("Cliff Settings", EditorStyles.boldLabel, GUILayout.Width (Screen.width - showLabelWidth - scrollBarWidth));
				EditorGUILayout.LabelField (showCliffSettingsLabel, GUILayout.Width (53));
				hm.showCliffSettings = EditorGUILayout.Foldout (hm.showCliffSettings, "");
				EditorGUILayout.EndHorizontal ();
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x, GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, Screen.width - 5 - scrollBarWidth, 2), color08);
			
				if (hm.showCliffSettings) {
					EditorGUILayout.GetControlRect (GUILayout.Height (0));
				
					// Cliff Settings Content
				
					// Diffuse Texture
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Colormap", tTipCliffMainColormap), EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					Rect rectA = GUILayoutUtility.GetRect (0, 0);
					rectA = new Rect (rectA.x, rectA.y, 129, 129);
					hm.hTexCliffDiff = EditorGUI.ObjectField (rectA, hm.hTexCliffDiff, typeof(Texture), false) as Texture;
					EditorGUILayout.EndHorizontal ();

					// A is Gloss
					if (hm.hFeatDirSpec || hm.hFeatCubeRefl) {
						EditorGUILayout.BeginHorizontal ();
						hm.hSetCliffAIsGlossMask = EditorGUILayout.Toggle (hm.hSetCliffAIsGlossMask, GUILayout.Width (12));
						EditorGUILayout.LabelField (new GUIContent("A = Gloss", tTipCliffAIsGlossMask), EditorStyles.label, GUILayout.Width (85));
						EditorGUILayout.EndHorizontal ();
					} else {
						GUILayout.Box ("", EditorStyles.label, GUILayout.Width (106));
					}
				
					// A is Emission
					if (hm.hFeatEmissivness) {
						EditorGUILayout.BeginHorizontal ();
						hm.hSetCliffAIsEmissMask = EditorGUILayout.Toggle (hm.hSetCliffAIsEmissMask, GUILayout.Width (12));
						EditorGUILayout.LabelField (new GUIContent("A = Emission", tTipCliffAIsEmissMask), EditorStyles.label, GUILayout.Width (85));
						EditorGUILayout.EndHorizontal ();
					} else {
						GUILayout.Box ("", EditorStyles.label, GUILayout.Width (106));
					}
				
					EditorGUILayout.GetControlRect (GUILayout.Height (76));
				
					// Normal Texture
					if (hm.hFeatNormalmaps) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Normalmap", tTipCliffMainNormalmap), EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						Rect rectB = EditorGUILayout.GetControlRect ();
						rectB = new Rect (rectB.x, rectB.y, 128, 16);
						hm.hTexCliffNM = EditorGUI.ObjectField (rectB, hm.hTexCliffNM, typeof(Texture), false) as Texture;
						EditorGUILayout.EndHorizontal ();
					}
				
					// Map Scale
					EditorGUILayout.BeginHorizontal ();
					ButtonDragSingle ("Tiling", "", "hSetCliffScaleOffset", 0.05f, 2);
					//EditorGUILayout.LabelField ("Scale (m)", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetCliffScaleOffset.x = EditorGUILayout.FloatField (hm.hSetCliffScaleOffset.x, GUILayout.Width (63));
					hm.hSetCliffScaleOffset.y = EditorGUILayout.FloatField (hm.hSetCliffScaleOffset.y, GUILayout.Width (62));
					EditorGUILayout.EndHorizontal ();
				
					// Map Offset
	//				EditorGUILayout.BeginHorizontal ();
	//				ButtonDragDouble ("OffsetX", "OffsetY", "", "hSetCliffScaleOffset", "hSetCliffScaleOffset", 0.01f, 3, 4);
	//				//EditorGUILayout.LabelField ("Offset", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
	//				hm.hSetCliffScaleOffset.z = EditorGUILayout.FloatField (hm.hSetCliffScaleOffset.z, GUILayout.Width (63));
	//				hm.hSetCliffScaleOffset.w = EditorGUILayout.FloatField (hm.hSetCliffScaleOffset.w, GUILayout.Width (62));
	//				EditorGUILayout.EndHorizontal ();
				
					// Cliff Spec Gloss
					if (hm.hFeatDirSpec || hm.hFeatCubeRefl) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Spec/Gloss(A)", tTipCliffSpecGloss), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hColCliffSpecGloss = EditorGUILayout.ColorField (hm.hColCliffSpecGloss, GUILayout.Width (129));
						EditorGUILayout.EndHorizontal ();
					}
					// Cliff Detail Mode
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Cliff Detail Mode", tTipCliffDetailMode),EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
					hm.hSetCliffDetailMode = (HorizonMaster.HSetCliffDetailMode)EditorGUILayout.EnumPopup (hm.hSetCliffDetailMode, GUILayout.Width (129));
					EditorGUILayout.EndHorizontal ();

					// Cliff Detail Mode Warning
					//GUILayout.Box("Test", GUILayout.Width (Screen.width-5-scrollBarWidth), GUILayout.Height(40));
					bool isShowingWarning = false;
					if ((!hm.hFeatNormalmaps && hm.hSetCliffDetailMode == HorizonMaster.HSetCliffDetailMode.Normal) || ((hm.hFeatNormalmaps && hm.hSetCliffDetailMode == HorizonMaster.HSetCliffDetailMode.Normal) && (hm.hFeatDiffLightMode == HorizonMaster.DiffLightMode.CubeM))) {
						string warningMessage = "Test";
						if (!hm.hFeatNormalmaps && hm.hSetCliffDetailMode == HorizonMaster.HSetCliffDetailMode.Normal) {
							warningMessage = "You need to enable normalmapping in the \"Horizon[ON] Features\" section!";
						}
						if (hm.hFeatNormalmaps && hm.hSetCliffDetailMode == HorizonMaster.HSetCliffDetailMode.Normal && hm.hFeatDiffLightMode == HorizonMaster.DiffLightMode.CubeM) {
							warningMessage = "Cliff Detail Normalmapping is not available with Diffuse Lighting Mode of type Cubemap!";
						}
						Rect rectWarning = GUILayoutUtility.GetRect (0, 0);
						EditorGUI.HelpBox (new Rect (rectWarning.x, rectWarning.y + 1, Screen.width - 4 - scrollBarWidth, 40), warningMessage, MessageType.Warning);
						EditorGUILayout.GetControlRect (GUILayout.Height (40));
						isShowingWarning = true;
					}

					if (hm.hSetCliffDetailMode != HorizonMaster.HSetCliffDetailMode.None) {
						// Colormap Strength
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Color Intensity", tTipCliffDiffIntens), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetCliffDetDiffIntens = EditorGUILayout.Slider (hm.hSetCliffDetDiffIntens, 0, 1, GUILayout.Width (129)); // Remember to fix Slider Range
						EditorGUILayout.EndHorizontal ();

						// Normal Strength
						if (!isShowingWarning) {
							if (hm.hFeatNormalmaps && hm.hSetCliffDetailMode == HorizonMaster.HSetCliffDetailMode.Normal) {
								EditorGUILayout.BeginHorizontal ();
								EditorGUILayout.LabelField (new GUIContent("Normal Intensity", tTipCliffNMIntens),EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
								hm.hSetCliffDetNMIntens = EditorGUILayout.Slider (hm.hSetCliffDetNMIntens, 0, 1, GUILayout.Width (129)); // Remember to fix Slider Range
								EditorGUILayout.EndHorizontal ();
							}
						}

						// Detail Diffuse Texture
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Colormap", tTipCliffDetDiff),EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						Rect rectC = GUILayoutUtility.GetRect (0, 0);
						rectC = new Rect (rectC.x, rectC.y, 129, 129);
						hm.hTexCliffDetDiff = EditorGUI.ObjectField (rectC, hm.hTexCliffDetDiff, typeof(Texture), false) as Texture;
						EditorGUILayout.EndHorizontal ();

						GUILayout.Box ("", EditorStyles.label, GUILayout.Width (106));
						GUILayout.Box ("", EditorStyles.label, GUILayout.Width (106));
						EditorGUILayout.GetControlRect (GUILayout.Height (76));
					
						// Normal Texture
						if (!isShowingWarning) {
							if (hm.hFeatNormalmaps && hm.hSetCliffDetailMode == HorizonMaster.HSetCliffDetailMode.Normal) {
								EditorGUILayout.BeginHorizontal ();
								EditorGUILayout.LabelField (new GUIContent("Normalmap", tTipCliffDetNM), EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
								Rect rectD = EditorGUILayout.GetControlRect ();
								rectD = new Rect (rectD.x, rectD.y, 128, 16);
								hm.hTexCliffDetNM = EditorGUI.ObjectField (rectD, hm.hTexCliffDetNM, typeof(Texture), false) as Texture;
								EditorGUILayout.EndHorizontal ();
							}
						}
					
						// Map Scale
						EditorGUILayout.BeginHorizontal ();
						ButtonDragSingle ("Tiling", "", "hSetCliffDetScaleOffset", 0.1f, 2);
						//EditorGUILayout.LabelField ("Scale (m)", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.hSetCliffDetScaleOffset.x = EditorGUILayout.FloatField (hm.hSetCliffDetScaleOffset.x, GUILayout.Width (63));
						hm.hSetCliffDetScaleOffset.y = EditorGUILayout.FloatField (hm.hSetCliffDetScaleOffset.y, GUILayout.Width (62));
						EditorGUILayout.EndHorizontal ();
					
	//					// Map Offset
	//					EditorGUILayout.BeginHorizontal ();
	//					EditorGUILayout.LabelField ("Offset (m)", EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
	//					hm.hSetCliffDetScaleOffset.z = EditorGUILayout.FloatField (hm.hSetCliffDetScaleOffset.z, GUILayout.Width (63));
	//					hm.hSetCliffDetScaleOffset.w = EditorGUILayout.FloatField (hm.hSetCliffDetScaleOffset.w, GUILayout.Width (62));
	//					EditorGUILayout.EndHorizontal ();
					}
				}
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x + (Screen.width - 5 - scrollBarWidth), GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, 1 - (Screen.width - 5 - scrollBarWidth), 2), color08);
				EditorGUILayout.Space ();
			}


	//===== Tools Settings ======================================================================================================================================================================

			if (hm.horizonChildAvailable) {

				// Tooltips
				string tTipToolsSetFeatures = 
					"When this is enabled horizon master will set the features for all of its children.\n(Default: On)";
				string tTipToolsGetFeatures = 
					"If this is enabled this script will adopt the material features of its children instead of being \"set only\". "+
						"This is needed if you want to control this horizon master with a horizon master higher up in the hierarchy. " +
						"For example if you want to control material groups with differentfeature sets.\n(Default: Off)";
				string tTipToolsGetMatSettingsfromCh = 
					"If this is enabled this script will adopt the material parameters of its children instead of being \"set only\". " +
						"This is needed if you want to control this script with a horizon master higher up in the hierarchy. " +
						"For example if you want to control material groups with different feature sets.\n(Default: Off)";
				string tTipToolsHideWireFrame = 
					"Hides the annoying wireframes for the children, so you can see what you are tweaking.\n(Default: Off)";
				string tTipToolsGetSettingsFromMat =
					"Drag a Horizon[ON] Material into the slot to adopt its settings.";
				string tTipToolsLoad = 
					"Load a previously saved settings file.";
				string tTipToolsSave = 
					"Save the current settings to a specified location.";
				string tTipToolsShowBounds = 
					"Show the bounds of the children which use displacement, this helps to check if culling is done right.\n(Default: Off)";
				string tTipToolsCalcBounds = 
					"Automatically calculate the bounds of children which use displacement, this should always be done if you are satisfied with the results, Culling will be more efficient and correct.";
				string tTipToolsBakeDisp = 
					"This bakes the displacement of the children into new meshes. This is helpful if you want to use them on mobile where displacement does not work, " +
						"or if you need to add meshcolliders to them.";

			// Tools Header
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.LabelField ("Horizon[ON] Tools", EditorStyles.boldLabel, GUILayout.Width (Screen.width - showLabelWidth - scrollBarWidth));
				EditorGUILayout.LabelField (showToolsLabel, GUILayout.Width (53));
				hm.showTools = EditorGUILayout.Foldout (hm.showTools, "");
				EditorGUILayout.EndHorizontal ();
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x, GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, Screen.width - 5 - scrollBarWidth, 2), color10);

				if (hm.showTools) {
					EditorGUILayout.GetControlRect (GUILayout.Height (0));
				
					// Tools Content

					// Options
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField (new GUIContent("Set Features (for all children)", tTipToolsSetFeatures), GUILayout.Width (Screen.width - 23 - scrollBarWidth));
					hm.setFeatures = EditorGUILayout.Toggle(hm.setFeatures, GUILayout.Width (16));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField (new GUIContent("Get Features (from children)", tTipToolsGetFeatures), GUILayout.Width (Screen.width - 23 - scrollBarWidth));
					hm.getFeatures = EditorGUILayout.Toggle(hm.getFeatures, GUILayout.Width (16));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField (new GUIContent("Get Material Settings (from children)", tTipToolsGetMatSettingsfromCh), GUILayout.Width (Screen.width - 23 - scrollBarWidth));
					hm.getMatSettings = EditorGUILayout.Toggle(hm.getMatSettings, GUILayout.Width (16));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField (new GUIContent("Hide Selection Wireframe", tTipToolsHideWireFrame), GUILayout.Width(Screen.width - 23 - scrollBarWidth));
					bool changedOld = GUI.changed;
					GUI.changed = false;
					hm.showWireF = EditorGUILayout.Toggle (hm.showWireF);
					if (GUI.changed) hm.ShowWireFrame(hm.showWireF);
					GUI.changed = changedOld;
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField (new GUIContent("Get Settings from Material:", tTipToolsGetSettingsFromMat), EditorStyles.boldLabel, GUILayout.Width (Screen.width - 137 - scrollBarWidth));

					Rect rectB = EditorGUILayout.GetControlRect ();
					rectB = new Rect (rectB.x, rectB.y, 128, 16);
					hm.getFromMaterialMat = EditorGUI.ObjectField (rectB, hm.getFromMaterialMat, typeof(Material), false) as Material;
					EditorGUILayout.EndHorizontal ();

					// Load / Save
					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent("Load Settings", tTipToolsLoad), GUILayout.Width (Screen.width - 137 - scrollBarWidth)))
						hm.LoadPreset (EditorUtility.OpenFilePanel ("Load Preset", installPath + "/Presets", "prefab"));
					if (GUILayout.Button (new GUIContent("Save Settings", tTipToolsSave), GUILayout.Width (128)))
						hm.SavePreset (EditorUtility.SaveFilePanel ("Save Preset", installPath + "/Presets", "MyPreset1", "prefab"));  
					EditorGUILayout.EndHorizontal ();

					if (hm.childUsesDisplacement) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField (new GUIContent("Show Bounds", tTipToolsShowBounds), EditorStyles.label, GUILayout.Width (Screen.width - 137 - scrollBarWidth));
						hm.drawGizmos = EditorGUILayout.Toggle (hm.drawGizmos, GUILayout.Width (12));
						hm.gizmoColor = EditorGUILayout.ColorField (hm.gizmoColor, GUILayout.Width (129 - 17));
						EditorGUILayout.EndHorizontal ();

						//GUI.backgroundColor = new Color (0.4f, 0.5f, 0.4f, 0.4f);
						if (GUILayout.Button (new GUIContent("Set Bounds of displaced Meshes", tTipToolsCalcBounds), GUILayout.Width (Screen.width - 4 - scrollBarWidth))) { hm.GetBounds (); }
						if (GUILayout.Button (new GUIContent("Bake displacement into Meshes", tTipToolsBakeDisp), GUILayout.Width (Screen.width - 4 - scrollBarWidth))) {
							if (EditorUtility.DisplayDialog ("Note", "This will bake the displacement into the displaced meshes. Are you sure you want to do this?\n(The source meshes in the project wont be affected)", "OK", "Cancel")) {
								hm.Displace ();
							}
						}
					}
				}
				GUI.DrawTexture (new Rect (GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).x + (Screen.width - 5 - scrollBarWidth), GUILayoutUtility.GetRect (Screen.width - 5 - scrollBarWidth, 2).y, 1 - (Screen.width - 5 - scrollBarWidth), 2), color10);
				EditorGUILayout.Space ();
			}

			if (hm.isPreset) {
				Rect rectWarning = GUILayoutUtility.GetRect (0, 0);
				EditorGUI.HelpBox (new Rect (rectWarning.x, rectWarning.y + 1, Screen.width - 4 - scrollBarWidth, 45), "This is a preset, you can load it by using the \"Load preset\" button on your Horizon[ON] object!", MessageType.Info);
				EditorGUILayout.GetControlRect (GUILayout.Height (45));
			}


			//EditorGUILayout.Space ();
			//drawInspector = EditorGUILayout.ToggleLeft ("Draw DefaultInspector", drawInspector);
			//if (drawInspector) DrawDefaultInspector ();

	//==================================================================================================================================================================================

			if (drag) {
				GUI.changed = true;
			}

			if (GUI.changed) {
				if (hm.getFromMaterialMat != null) {
					hm.UpdateHorizonMaster();
					hm.getFromMaterialMat = null;
					hm.UpdateMaterials();
					hm.CheckMaterials();
				}
				hm.UpdateMaterials();
				SceneView.RepaintAll();
			}
		}

		void ButtonDragDouble(string label1, string label2, string label3, string change1, string change2, float sens = 1, int dragV1 = 1, int dragV2 = 1, int i = -1, string ctrlID = ""){
			EditorGUILayout.LabelField("", GUILayout.Width (Screen.width - 137 - scrollBarWidth));
			Rect rect = GUILayoutUtility.GetLastRect ();
			Vector2 v2 = GUI.skin.GetStyle ("boldLabel").CalcSize (new GUIContent (label1));
			Rect labelRect = new Rect (rect.x, rect.y, v2.x, v2.y);
			EditorGUI.LabelField (labelRect, label1, (valueToChange == change1 && activeCtrlID == ctrlID+"1") || (valueToChange == change1 && ctrlID == "") ? EditorStyles.boldLabel : EditorStyles.label);

			if (e.type == EventType.MouseDown) {
				if (labelRect.Contains (e.mousePosition)) { 
					drag = true; 
					dragVector = dragV1; 
					sensitivity = sens; 
					valueToChange = change1; 
					Repaint(); 
					if (i != -1) hmlp = hm.hSetLayerProps[i];
					activeCtrlID = ctrlID+"1";
				}
			}
			EditorGUI.LabelField(new Rect (labelRect.xMax-3, labelRect.y, 9,16),"/", EditorStyles.label);
			v2 = GUI.skin.GetStyle ("boldLabel").CalcSize (new GUIContent (label2));
			labelRect = new Rect (labelRect.xMax + 5, labelRect.y, v2.x, v2.y);
			EditorGUI.LabelField (labelRect, label2, (valueToChange == change2 && activeCtrlID == ctrlID+"2") || (valueToChange == change2 && ctrlID == "") ? EditorStyles.boldLabel : EditorStyles.label);
			if (e.type == EventType.MouseDown) {
				if (labelRect.Contains (e.mousePosition)) { 
					drag = true; 
					dragVector = dragV2; 
					sensitivity = sens; 
					valueToChange = change2; 
					Repaint();
					if (i != -1) hmlp = hm.hSetLayerProps[i];
					activeCtrlID = ctrlID+"2";
				}
			}
			v2 = GUI.skin.GetStyle ("Label").CalcSize (new GUIContent (label3));
			EditorGUI.LabelField(new Rect(labelRect.xMax-3,labelRect.y,v2.x,v2.y),label3, EditorStyles.label);
		}
		void ButtonDragSingle(string label1, string label2, string change1, float sens = 1, int dragV = 1, int i = -1, string ctrlID = ""){
			EditorGUILayout.LabelField("", GUILayout.Width (Screen.width - 137 - scrollBarWidth));
			Rect rect = GUILayoutUtility.GetLastRect ();
			Vector2 v2 = GUI.skin.GetStyle ("boldLabel").CalcSize (new GUIContent (label1));
			Rect labelRect = new Rect (rect.x, rect.y, v2.x, v2.y);
			EditorGUI.LabelField (labelRect, label1, (valueToChange == change1 && activeCtrlID == ctrlID) || (valueToChange == change1 && ctrlID == "") ? EditorStyles.boldLabel : EditorStyles.label);
			
			if (e.type == EventType.MouseDown) {
				if (labelRect.Contains (e.mousePosition)) { 
					drag = true; 
					dragVector = dragV; 
					sensitivity = sens; 
					valueToChange = change1; 
					Repaint();
					if (i != -1) hmlp = hm.hSetLayerProps[i];
					activeCtrlID = ctrlID;			
				}

			}
			v2 = GUI.skin.GetStyle ("Label").CalcSize (new GUIContent (label2));
			EditorGUI.LabelField(new Rect(labelRect.xMax-3,labelRect.y,v2.x,v2.y),label2, EditorStyles.label);
		}
	}
}