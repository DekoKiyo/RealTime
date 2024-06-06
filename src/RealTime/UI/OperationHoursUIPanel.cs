namespace RealTime.UI
{
    using ColossalFramework;
    using ColossalFramework.UI;
    using RealTime.CustomAI;
    using UnityEngine;

    public static class OperationHoursUIPanel
    {
        public static UIPanel m_uiMainPanel;

        private static ZonedBuildingWorldInfoPanel m_zonedBuildingWorldInfoPanel;

        private static UILabel m_settingsHeader;
        private static UILabel m_settingsTitle;
        private static UICheckBox m_settingsCheckBox;

        private static UICheckBox m_workAtNight;
        private static UICheckBox m_workAtWeekands;
        private static UICheckBox m_hasExtendedWorkShift;
        private static UICheckBox m_hasContinuousWorkShift;
        private static UISlider m_workShifts;

        private static UIButton SaveOperationHoursBtn;

        private static readonly float DEFAULT_HEIGHT = 18F;

        public static void Init() => CreateUI();

        private static void CreateUI()
        {
            m_zonedBuildingWorldInfoPanel = GameObject.Find("(Library) ZonedBuildingWorldInfoPanel").GetComponent<ZonedBuildingWorldInfoPanel>();
            var makeHistoricalPanel = m_zonedBuildingWorldInfoPanel.Find("MakeHistoricalPanel").GetComponent<UIPanel>();
            if (makeHistoricalPanel != null)
            {
                m_uiMainPanel = m_zonedBuildingWorldInfoPanel.component.AddUIComponent<UIPanel>();
                m_uiMainPanel.name = "OperationHoursUIPanel";
                m_uiMainPanel.backgroundSprite = "SubcategoriesPanel";
                m_uiMainPanel.opacity = 0.90f;
                m_uiMainPanel.isVisible = false;
                m_uiMainPanel.relativePosition = new Vector3(m_uiMainPanel.parent.width + 1f, 40f);
                m_uiMainPanel.height = 370f;
                m_uiMainPanel.width = 310f;

                m_settingsCheckBox = UiUtils.CreateCheckBox(makeHistoricalPanel, "SettingsCheckBox", "settings", false);
                m_settingsCheckBox.width = 110f;
                m_settingsCheckBox.label.textColor = new Color32(185, 221, 254, 255);
                m_settingsCheckBox.label.textScale = 0.8125f;
                m_settingsCheckBox.tooltip = "change building operation hours.";
                m_settingsCheckBox.AlignTo(m_zonedBuildingWorldInfoPanel.component, UIAlignAnchor.TopLeft);
                m_settingsCheckBox.relativePosition = new Vector3(350f, 6f);
                m_settingsCheckBox.eventCheckChanged += (component, value) =>
                {
                    m_uiMainPanel.isVisible = value;
                    m_uiMainPanel.height = m_uiMainPanel.parent.height - 7f;
                };
                makeHistoricalPanel.AttachUIComponent(m_settingsCheckBox.gameObject);

                m_settingsHeader = UiUtils.CreateLabel(m_uiMainPanel, "SettingsPanelHeader", "Settings", "");
                m_settingsHeader.font = UiUtils.GetUIFont("OpenSans-Regular");
                m_settingsHeader.textAlignment = UIHorizontalAlignment.Center;
                m_settingsHeader.relativePosition = new Vector3(30f, 60f + 0 * (DEFAULT_HEIGHT * 0.8f + 2f));

                m_settingsTitle = UiUtils.CreateLabel(m_uiMainPanel, "SettingsTitle", "Adjust Operation Hours", "");
                m_settingsTitle.font = UiUtils.GetUIFont("OpenSans-Regular");
                m_settingsTitle.textAlignment = UIHorizontalAlignment.Center;
                m_settingsTitle.textColor = new Color32(78, 184, 126, 255);
                m_settingsTitle.relativePosition = new Vector3(45f, 30f);
                m_settingsTitle.textScale = 1.2f;

                m_workAtNight = UiUtils.CreateCheckBox(m_uiMainPanel, "WorkAtNight", "Work At Night", false);
                m_workAtNight.width = 110f;
                m_workAtNight.label.textColor = new Color32(185, 221, 254, 255);
                m_workAtNight.label.textScale = 0.8125f;
                m_workAtNight.tooltip = "choose if the building will work at night.";
                m_workAtNight.AlignTo(m_zonedBuildingWorldInfoPanel.component, UIAlignAnchor.TopLeft);
                m_workAtNight.relativePosition = new Vector3(30f, 110f);
                m_workAtNight.eventCheckChanged += (component, value) =>
                {
                    m_workAtNight.isChecked = value;
                    UpdateSlider();
                };
                m_uiMainPanel.AttachUIComponent(m_workAtNight.gameObject);

                m_workAtWeekands = UiUtils.CreateCheckBox(m_uiMainPanel, "WorkAtWeekands", "Work At Weekands", false);
                m_workAtWeekands.width = 110f;
                m_workAtWeekands.label.textColor = new Color32(185, 221, 254, 255);
                m_workAtWeekands.label.textScale = 0.8125f;
                m_workAtWeekands.tooltip = "choose if the building will work at weekends.";
                m_workAtWeekands.AlignTo(m_zonedBuildingWorldInfoPanel.component, UIAlignAnchor.TopLeft);
                m_workAtWeekands.relativePosition = new Vector3(30f, 150f);
                m_workAtWeekands.eventCheckChanged += (component, value) => m_workAtWeekands.isChecked = value;
                m_uiMainPanel.AttachUIComponent(m_workAtWeekands.gameObject);

                m_hasExtendedWorkShift = UiUtils.CreateCheckBox(m_uiMainPanel, "HasExtendedWorkShift", "Has Extended Work Shift", false);
                m_hasExtendedWorkShift.width = 110f;
                m_hasExtendedWorkShift.label.textColor = new Color32(185, 221, 254, 255);
                m_hasExtendedWorkShift.label.textScale = 0.8125f;
                m_hasExtendedWorkShift.tooltip = "choose if the building will have an extended work shift.";
                m_hasExtendedWorkShift.AlignTo(m_zonedBuildingWorldInfoPanel.component, UIAlignAnchor.TopLeft);
                m_hasExtendedWorkShift.relativePosition = new Vector3(30f, 190f);
                m_hasExtendedWorkShift.eventCheckChanged += (component, value) =>
                {
                    m_hasExtendedWorkShift.isChecked = value;
                    UpdateSlider();
                };
                m_uiMainPanel.AttachUIComponent(m_hasExtendedWorkShift.gameObject);

                m_hasContinuousWorkShift = UiUtils.CreateCheckBox(m_uiMainPanel, "HasContinuousWorkShift", "Has Continuous Work Shift", false);
                m_hasContinuousWorkShift.width = 110f;
                m_hasContinuousWorkShift.label.textColor = new Color32(185, 221, 254, 255);
                m_hasContinuousWorkShift.label.textScale = 0.8125f;
                m_hasContinuousWorkShift.tooltip = "choose if the building will have a continuous work shift.";
                m_hasContinuousWorkShift.AlignTo(m_zonedBuildingWorldInfoPanel.component, UIAlignAnchor.TopLeft);
                m_hasContinuousWorkShift.relativePosition = new Vector3(30f, 230f);
                m_hasContinuousWorkShift.eventCheckChanged += (component, value) =>
                {
                    m_hasContinuousWorkShift.isChecked = value;
                    UpdateSlider();
                };
                m_uiMainPanel.AttachUIComponent(m_hasContinuousWorkShift.gameObject);

                m_workShifts = UiUtils.CreateSlider(m_uiMainPanel, "ShiftCount", 1, 3, 1, 1);
                m_workShifts.AlignTo(m_zonedBuildingWorldInfoPanel.component, UIAlignAnchor.TopLeft);
                m_workShifts.relativePosition = new Vector3(30f, 270f);
                m_uiMainPanel.AttachUIComponent(m_workShifts.gameObject);

                SaveOperationHoursBtn = UiUtils.AddButton(m_uiMainPanel, 30f, 280f, "SaveOperationHours", "Save Operation Hours", "save building working hours");
                SaveOperationHoursBtn.eventClicked += SaveOperationHours;
            }
        }

        private static void UpdateSlider()
        {
            if(m_hasExtendedWorkShift.isChecked)
            {
                m_hasContinuousWorkShift.isChecked = false;
            }
            if (m_hasContinuousWorkShift.isChecked)
            {
                m_workShifts.maxValue = m_workAtNight.isChecked ? 2 : 1;
                m_hasExtendedWorkShift.isChecked = false;
            }
            else
            {
                m_workShifts.maxValue = m_workAtNight.isChecked ? 3 : 2;
            }
        }

        public static void RefreshData()
        {
            ushort buildingID = WorldInfoPanel.GetCurrentInstanceID().Building;
            var building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID];
            var buildingAI = building.Info.GetAI();
            if (buildingAI is not CommercialBuildingAI)
            {
                m_settingsCheckBox.Hide();
                m_uiMainPanel.Hide();
            }
            else
            {
                if (BuildingWorkTimeManager.BuildingsWorkTime.TryGetValue(buildingID, out var buildingWorkTime))
                {
                    m_workAtNight.isChecked = buildingWorkTime.WorkAtNight;
                    m_workAtWeekands.isChecked = buildingWorkTime.WorkAtWeekands;
                    m_hasExtendedWorkShift.isChecked = buildingWorkTime.HasExtendedWorkShift;
                    m_hasContinuousWorkShift.isChecked = buildingWorkTime.HasContinuousWorkShift;
                    m_workShifts.value = buildingWorkTime.WorkShifts;
                }
                m_settingsCheckBox.Show();
                if (m_settingsCheckBox.isChecked)
                {
                    m_uiMainPanel.height = 370f;
                    m_uiMainPanel.Show();
                }
            }
        }

        private static void SaveOperationHours(UIComponent c, UIMouseEventParameter eventParameter) => SaveSettings();

        private static void SaveSettings()
        {
            ushort buildingID = WorldInfoPanel.GetCurrentInstanceID().Building;

            var buildingWorkTime = BuildingWorkTimeManager.GetBuildingWorkTime(buildingID);

            buildingWorkTime.WorkAtNight = m_workAtNight;
            buildingWorkTime.WorkAtWeekands = m_workAtWeekands.isChecked;
            buildingWorkTime.HasExtendedWorkShift = m_hasExtendedWorkShift.isChecked;
            buildingWorkTime.HasContinuousWorkShift = m_hasContinuousWorkShift.isChecked;
            buildingWorkTime.WorkShifts = (int)m_workShifts.value;

            BuildingWorkTimeManager.SetBuildingWorkTime(buildingID, buildingWorkTime);

            RefreshData();
        }

        private static void LoadSettings(ushort buildingID)
        {

            if (BuildingWorkTimeManager.BuildingsWorkTime.TryGetValue(buildingID, out var buildingWorkTime))
            {
                m_workAtNight.isChecked = buildingWorkTime.WorkAtNight;
                m_workAtWeekands.isChecked = buildingWorkTime.WorkAtWeekands;
                m_hasExtendedWorkShift.isChecked = buildingWorkTime.HasExtendedWorkShift;
                m_hasContinuousWorkShift.isChecked = buildingWorkTime.HasContinuousWorkShift;
                m_workShifts.value = buildingWorkTime.WorkShifts;
            }

        }
    }

}