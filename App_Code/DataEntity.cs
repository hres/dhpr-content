using System;
using System.Collections.Generic;



namespace dhpr
{
    public enum programType
    {
        rds,
        ssr,
        sbd,
        sbdmd,
        rdsmd,
        dhpr
    }

    public class regulatoryDecisionItem
    {
        public int id { get; set; }
        public string link_id { get; set; }
        public string drug_name { get; set; }
        public string contact_name { get; set; }
        public string contact_url { get; set; }
        public string medical_ingredient { get; set; }
        public string therapeutic_area { get; set; }
        public string purpose { get; set; }
        public string reason_decision { get; set; }      
        public string decision { get; set; }
        public string decision_descr { get; set; }
        public DateTime? date_decision { get; set; }
        public string manufacturer { get; set; }
        public string prescription_status { get; set; }
        public string type_submission { get; set; }
        public DateTime? date_filed { get; set; }
        public string control_number { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public string foot_notes { get; set; }
        public List<string> din_list { get; set; }
        public List<BulletPoint> bullet_list { get; set; }
    }

    public class regulatoryDecisionMedicalDevice
    {
        public string link_id { get; set; }
        public string drug_name { get; set; }
        public string contact_name { get; set; }
        public string contact_url { get; set; }
        public string decision { get; set; }
        public string decision_descr { get; set; }
        public DateTime? date_decision { get; set; }
        public string type_application { get; set; }
        public DateTime? date_filed { get; set; }
        public int application_number { get; set; }
        public bool is_md { get; set; }
        public string device_class { get; set; }
        public string what_app_for { get; set; }
        public string info_reviewed { get; set; }
        public string licence_num_issued { get; set; }
        public string manufacturer { get; set; }
        public string medical_ingredient { get; set; }
    }

    public class rdsSearchItem
    {
        public string link_id { get; set; }
        public string drug_name { get; set; }
        public string medical_ingredient { get; set; }
        public string manufacturer { get; set; }
        public string decision { get; set; }
        public DateTime? date_decision { get; set; }
        public string control_number { get; set; }
        public string type_submission { get; set; }
        public List<string> din_list { get; set; }
        public string din { get; set; }
        public bool is_md { get; set; }
    }

    public class summarySafetyItem
    {
        public string link_id { get; set; }
        public string drug_name { get; set; }
        public string safety_issue { get; set; }
        public string issue { get; set; }
        public string background { get; set; }
        public string objective { get; set; }        
        public string key_findings { get; set; }  
        public string additional { get; set; }
        public string full_review { get; set; }
        public string sr_references { get; set; }
        public string foot_notes { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public int template { get; set; }
        public List<BulletPoint> conclusion_list { get; set; }
        public List<BulletPoint> key_message_list { get; set; }
        public List<BulletPoint> use_canada_list { get; set; }
        public List<BulletPoint> finding_list { get; set; }        
        public string overview { get; set; }
        public List<BulletPoint> footnotes_list { get; set; }
        public List<BulletPoint> reference_list { get; set; }
    }
    public class ssrSearchItem
    {
        public string link_id { get; set; }
        public string drug_name { get; set; }
        public string safety_issue { get; set; }
        public DateTime? created_date { get; set; }
        public int template { get; set; }

    }
    public class BulletPoint
    {
        public int field_id { get; set; }
        public int order_no { get; set; }
        public string bullet { get; set; }
    }
    public class sbdSearchItem
    {
        public string link_id { get; set; }
        public string brand_name { get; set; }       
        public string med_ingredient { get; set; }
        public string manufacturer { get; set; }
        public DateTime? date_issued { get; set; }
        public int template { get; set; }
        public string licence_number { get; set; }
        public bool is_md { get; set; }
        public string din { get; set; }
        private readonly List<string> _dinList = new List<string>();
        public IList<string> din_list { get { return _dinList; } }
    }

    public class summaryBasisItem
    {
        public string link_id { get; set; }
        public string template { get; set; }
        public string brand_name { get; set; } 
        public string manufacturer { get; set; }               
        public string control_number { get; set; }
        public DateTime? date_issued { get; set; }
        public string sub_type_number { get; set; }
        public DateTime? date_submission { get; set; }
        public DateTime? date_authorization { get; set; }
        public string notice_decision { get; set; }
        public string quality_basis { get; set; }
        public string sci_reg_decision { get; set; }
        public string benefit_risk { get; set; }
        public string non_clin_basis { get; set; }
        public string non_clin_basis2 { get; set; }
        public string clin_basis { get; set; }
        public string clin_basis1 { get; set; }
        public string clin_basis2 { get; set; }
        public string summary { get; set; }
        public string what_approved { get; set; }
        public string why_approved { get; set; }
        public string followup_measures { get; set; }
        public string steps_approval { get; set; }
        public string post_auth { get; set; }
        public string other_info { get; set; }
        public string a_clin_basis { get; set; }
        public string a_clin_basis2 { get; set; }
        public string a_non_clin_basis { get; set; }
        public string a_non_clin_basis2 { get; set; }
        public string b_quality_basis { get; set; }
        public string assess_basis { get; set; }
        public string contact { get; set; }
        public string med_ingredient { get; set; }
        //public string non_prop_name { get; set; }
        //public string strength { get; set; }
        //public string dosage_form { get; set; }
        //public string route_admin { get; set; }
        //public string bd_din_list { get; set; }
        //public string thera_class { get; set; }
        //public string non_med_ingredient { get; set; }
        public string paat_message { get; set; }
        public bool is_md { get; set; }
        public List<string> din_list { get; set; }
        public List<PostAuthActivity> post_activity_list { get; set; }
        public List<PostLicensingActivity> post_licensing_list { get; set; }
        public List<DecisionMilestone> milestone_list { get; set; }
        public List<Tombstone> tombstone_list { get; set; }
    }


    public class summaryBasisMDItem
    {
        public int template { get; set; }
        public string link_id { get; set; }
        public string device_name { get; set; }
        public string application_num { get; set; }
        public string recent_activity { get; set; }
        public DateTime? updated_date { get; set; }
        public string summary_basis_intro { get; set; }
        public string what_approved { get; set; }
        public string why_device_approved { get; set; }
        public string steps_approval_intro { get; set; }
        public string followup_measures { get; set; }
        public string post_licence_activity { get; set; }
        public string other_info { get; set; }
        public string scientific_rationale { get; set; }
        public string scientific_rationale2 { get; set; }
        public string scientific_rationale3 { get; set; }
        public DateTime? date_sbd_issued { get; set; }

        public string egalement { get; set; }
        public string manufacturer { get; set; }
        public string medical_device_group { get; set; }
        public string biological_material { get; set; }

        public string combination_product { get; set; }
        public string drug_material { get; set; }
        public string application_type_and_num { get; set; }
        public DateTime? date_licence_issued { get; set; }
        public string intended_use { get; set; }
        public string notice_of_decision { get; set; }
        public string sci_reg_basis_decision1 { get; set; }
        public string sci_reg_basis_decision2 { get; set; }
        public string sci_reg_basis_decision3 { get; set; }
        public string response_to_condition { get; set; }
        public string response_to_condition2 { get; set; }
        public string response_to_condition3 { get; set; }
        public string response_to_condition4 { get; set; }
        public string conclusion { get; set; }
        public string recommendation { get; set; }
        public string licence_number { get; set; }
        public bool is_md { get; set; }
        public List<PostLicensingActivity> plat_list { get; set; }
        public List<ApplicationMilestones> app_milestone_list { get; set; }

    }

    public class PostLicensingActivity
    {
        public string link_id { get; set; }
        public DateTime? date_submit { get; set; }
        public int num_order { get; set; }
        public string app_type_num { get; set; }
        public string decision_and_date { get; set; }
        public string summ_activity { get; set; }
    }

    public class PostAuthActivity
    { 
        public string link_id { get; set; }
        public DateTime? date_submit { get; set; }
        public DateTime? decision_start_date { get; set; }
        public DateTime? decision_end_date { get; set; }
        public int row_num { get; set; }
        public string act_contr_num { get; set; }
        public string submit_text { get; set; }
        public string paat_decision { get; set; }
        public string date_text { get; set; }
        public string summ_activity { get; set; }
    }

    public class DecisionMilestone
    {
        public string link_id { get; set; }
        public int num_order { get; set; }
        public string milestone { get; set; }
        public DateTime? completed_date { get; set; }
        public DateTime? completed_date2 { get; set; }
        public string separator { get; set; }
    }

    public class DecisionTombstone
    {
        public string link_id { get; set; }

    }
    public class ApplicationMilestones
    {

        public string link_id { get; set; }
        public int num_order { get; set; }
        public string application_milestone { get; set; }
        public DateTime? milestone_date { get; set; }
        public DateTime? milestone_date2 { get; set; }
        public string separator { get; set; }
    }

    public class Tombstone
    {

        public string link_id { get; set; }
        public int num_order { get; set; }
        public string med_ingredient { get; set; }
        public string nonprop_name { get; set; }
        public string strength { get; set; }
        public string dosageform { get; set; }
        public string route_admin { get; set; }
        public string thera_class { get; set; }
        public string nonmed_ingredient { get; set; }

    }  
}