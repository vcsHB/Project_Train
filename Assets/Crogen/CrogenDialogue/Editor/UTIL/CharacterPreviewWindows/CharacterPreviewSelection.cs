using Crogen.CrogenDialogue.Character;
using Crogen.CrogenDialogue.Editor.CharacterPreviewWindows;
using System;
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor.UTIL.CharacterPreviewWindows
{
	[InitializeOnLoad]
	public static class CharacterPreviewSelection
    {
		public static CharacterSO SelectedSO { get; set; }

		private static CharacterBodySO _selectedBodySO;
		private static CharacterElementInfo _selectedClothseInfo;
		private static CharacterElementInfo _selectedFaceInfo;

		public static CharacterBodySO SelectedBodySO { get => _selectedBodySO; set { _selectedBodySO = value; OnSelectBodyEvent?.Invoke(value.BodyInfo); }}
		public static CharacterElementInfo SelectedClothseInfo { get => _selectedClothseInfo; set { _selectedClothseInfo = value; OnSelectClothseEvent?.Invoke(value); }}
		public static CharacterElementInfo SelectedFaceInfo { get => _selectedFaceInfo; set { _selectedFaceInfo = value; OnSelectFaceEvent?.Invoke(value); }}

		public static Action<CharacterElementInfo> OnSelectBodyEvent;
		public static Action<CharacterElementInfo> OnSelectClothseEvent;
		public static Action<CharacterElementInfo> OnSelectFaceEvent;

		// Domain reload���� �ȵ�!
		static CharacterPreviewSelection()
		{
			Selection.selectionChanged += HandleSelectionChanged;
			EditorApplication.quitting += UnsubscribeEvents;
		}

		private static void UnsubscribeEvents()
		{
			Selection.selectionChanged -= HandleSelectionChanged;
			EditorApplication.quitting -= UnsubscribeEvents;
		}

		private static void HandleSelectionChanged()
		{
			SelectedSO = Selection.activeObject as CharacterSO;

			// window�� �������� ���� Rebuild�ϱ�
			var window = EditorWindow.HasOpenInstances<CharacterPreviewWindow>()
				   ? EditorWindow.GetWindow<CharacterPreviewWindow>()
				   : null;

			window?.RebuildGUI(SelectedSO);
		}

		[UnityEditor.Callbacks.OnOpenAsset]
		public static bool OnOpenAsset(int instanceID, int line)
		{
			var obj = EditorUtility.InstanceIDToObject(instanceID);
			if (obj is CharacterSO characterSO)
			{
				// EditorWindow ����
				var window = EditorWindow.GetWindow<CharacterPreviewWindow>();
				window.titleContent = new GUIContent("CharacterPreviewWindow");

				SelectedSO = characterSO;

				window.RebuildGUI(characterSO);

				return true; // ó�������� Unity�� �˸�
			}

			return false; // Unity �⺻ ���� ����
		}
	}
}
