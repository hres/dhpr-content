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
        dhpr
    }


    public class regulatoryDecisionItem
    {
        public int Id { get; set; }
        public string LinkId { get; set; }
        public string Drugname { get; set; }
        public string ContactName { get; set; }
        public string ContactUrl { get; set; }
        public string MedicalIngredient { get; set; }
        public string TherapeuticArea { get; set; }
        public string Purpose { get; set; }
        public string ReasonDecision { get; set; }      
        public string Decision { get; set; }
        public string DecisionDescr { get; set; }
        public DateTime? DateDecision { get; set; }
        public string Manufacture { get; set; }
        public string PrescriptionStatus { get; set; }
        public string TypeSubmission { get; set; }
        public DateTime? DateFiled { get; set; }
        public string ControlNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Footnotes { get; set; }
        public List<string> DinList { get; set; }
        public List<BulletPoint> BulletList { get; set; }

    }
    public class rdsSearchItem
    {
        public string LinkId { get; set; }
        public string Drugname { get; set; }
        public string MedicalIngredient { get; set; }
        public string Manufacture { get; set; }
        public string Decision { get; set; }
        public DateTime? DateDecision { get; set; }
        public string ControlNumber { get; set; }
        public string TypeSubmission { get; set; }
        public List<string> DinList { get; set; }
        public string Din { get; set; }
    }

    public class summarySafetyItem
    {
        public string LinkId { get; set; }
        public string DrugName { get; set; }
        public string Safetyissue { get; set; }
        public string Issue { get; set; }
        public string Background { get; set; }
        public string Objective { get; set; }        
        public string KeyFindings { get; set; }  
        public string Additional { get; set; }
        public string FullReview { get; set; }
        public string SrReferences { get; set; }
        public string Footnotes { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Template { get; set; }
        public List<BulletPoint> ConclusionList { get; set; }
        public List<BulletPoint> KeyMessageList { get; set; }
        public List<BulletPoint> UseCanadaList { get; set; }
        public List<BulletPoint> FindingList { get; set; }        
        public string Overview { get; set; }
        public List<BulletPoint> FootnotesList { get; set; }
        public List<BulletPoint> ReferenceList { get; set; }
    }
    public class ssrSearchItem
    {
        public string LinkId { get; set; }
        public string Drugname { get; set; }
        public string Safetyissue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int Template { get; set; }

    }
    public class BulletPoint
    {
        public int FieldId { get; set; }
        public int OrderNo { get; set; }
        public string Bullet { get; set; }
    }
    public class sbdSearchItem
    {
        public string LinkId { get; set; }
        public string Brandname { get; set; }       
        public string MedIngredient { get; set; }
        public string Manufacturer { get; set; }
        public DateTime? DateIssued { get; set; }
        public int Template { get; set; }
        public string LicenseNo { get; set; }
        public bool isMd { get; set; }
        public string Din { get; set; }
        private readonly List<string> _dinList = new List<string>();
        public IList<string> DinList { get { return _dinList; } }
    }

    public class summaryBasisItem 
    {
        public string LinkId { get; set; }
        public string Template { get; set; }
        public string Brandname { get; set; }       
        public string MedIngredient { get; set; }
        public string Strength { get; set; }
        public string Manufacturer { get; set; }
        public string Dosageform { get; set; }        
        public string ControlNum { get; set; }
        public DateTime? DateIssued { get; set; }
        public string NonpropName { get; set; }
        public string RouteAdmin { get; set; }
        public string BdDinList { get; set; }
        public string TheraClass { get; set; }
        public string NonmedIngredient { get; set; }
        public string SubTypeNum { get; set; }
        public DateTime? DateSubmission { get; set; }
        public DateTime? DateAuthorization { get; set; }
        public string NoticeDecision { get; set; }
        public string QualityBasis { get; set; }
        public string SciRegDecision { get; set; }
        public string BenefitRisk { get; set; }
        public string NonclinBasis { get; set; }
        public string NonclinBasis2 { get; set; }
        public string ClinBasis { get; set; }
        public string ClinBasis1 { get; set; }
        public string ClinBasis2 { get; set; }
        public string Summary { get; set; }
        public string WhatApproved { get; set; }
        public string WhyApproved { get; set; }
        public string FollowupMeasures { get; set; }
        public string StepsApproval { get; set; }
        public string PostAuth { get; set; }
        public string OtherInfo { get; set; }
        public string AClinBasis { get; set; }
        public string AClinBasis2 { get; set; }
        public string ANonClinBasis { get; set; }
        public string ANonClinBasis2 { get; set; }
        public string BQualityBasis { get; set; }
        public string AssessBasis { get; set; }
        public string Contact { get; set; }
        public bool isMd { get; set; }
        public List<string> DinList { get; set; }
        public List<PostAuthActivity> PostActivityList { get; set; }
        public List<PostLicensingActivity> PostLicensingList { get; set; }
        public List<DecisionMilestone> MilestoneList { get; set; }
    }


    public class summaryBasisMDItem
    {
        public int Template { get; set; }
        public string LinkId { get; set; }
        public string DeviceName { get; set; }
        public string ApplicationNum { get; set; }
        public string RecentActivity { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string SummaryBasisIntro { get; set; }
        public string WhatApproved { get; set; }
        public string WhyDeviceApproved { get; set; }
        public string StepsApprovalIntro { get; set; }
        public string FollowupMeasures { get; set; }
        public string PostLicenceActivity { get; set; }
        public string OtherInfo { get; set; }
        public string ScientificRationale { get; set; }
        public string ScientificRationale2 { get; set; }
        public string ScientificRationale3 { get; set; }
        public DateTime? DateSbdIssued { get; set; }

        public string Egalement { get; set; }
        public string Manufacturer { get; set; }
        public string MedicalDeviceGroup { get; set; }
        public string BiologicalMaterial { get; set; }

        public string CombinationProduct { get; set; }
        public string DrugMaterial { get; set; }
        public string ApplicationTypeAndNum { get; set; }
        public DateTime? DateLicenceIssued { get; set; }
        public string IntendedUse { get; set; }
        public string NoticeOfDecision { get; set; }
        public string SciRegBasisDecision1 { get; set; }
        public string SciRegBasisDecision2 { get; set; }
        public string SciRegBasisDecision3 { get; set; }
        public string ResponseToCondition { get; set; }

        public string Conclusion { get; set; }
        public string Recommendation { get; set; }
        public string LicenseNo { get; set; }

        public bool isMd { get; set; }

        public List<PostLicensingActivity> PlatList { get; set; }
        public List<ApplicationMilestones> AppMilestoneList { get; set; }

    }

    public class PostLicensingActivity
    {
        public string LinkId { get; set; }
        public DateTime? DateSubmit { get; set; }

        public int NumOrder { get; set; }
        public string AppTypeNum { get; set; }

        public string DecisionAndDate { get; set; }
        public string SummActivity { get; set; }
    }
    public class PostAuthActivity
    {
        public string LinkId { get; set; }
        public DateTime? DateSubmit { get; set; }
        public DateTime? DecisionStartDate { get; set; }
        public DateTime? DecisionEndDate { get; set; }
        public int RowNum { get; set; }
        public string ActContrNum { get; set; }
        public string SubmitText { get; set; }
        public string PaatDecision { get; set; }
        public string DateText { get; set; }
        public string SummActivity { get; set; }
    }



    public class DecisionMilestone
    {
        public string LinkId { get; set; }
        public int NumOrder { get; set; }
        public string Milestone { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? CompletedDate2 { get; set; }
        public string Separator { get; set; }
    }

    public class ApplicationMilestones
    {

        public string LinkId { get; set; }
        public int NumOrder { get; set; }
        public string ApplicationMilestone { get; set; }
        public DateTime? MilestoneDate { get; set; }
        public DateTime? MilestoneDate2 { get; set; }
        public string Separator { get; set; }
    }
}