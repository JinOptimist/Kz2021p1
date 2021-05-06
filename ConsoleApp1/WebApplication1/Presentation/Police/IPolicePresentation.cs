using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.ViewModels;

namespace WebApplication1.Presentation.Police
{
	public interface IPolicePresentation
	{
		List<Citizen> GetAll();
		bool CitizenToJail(long id);
		bool AddedToPoliceAcademy(PoliceAcademy policeAcademy);
		long AddViolationForUser(Violations violation);
		bool AmnestySeverity(long id);
		bool CreateCall(PoliceCallHistory policeCallHistory);
		List<UserViolationViewModel> GetAllUserViolations(long id);
		//Task<UserInfoViewModel> GetUserInfo(long id);
		List<PolicemanViewModel> GetAllPolicemen();
		void DismissPoliceman(long id);
		List<ApplicantViewModel> GetAllApplicants();
		void AcceptApplicant(long id);
		long AddNewQuestion(Question question);
		void AddAnswers(List<Answer> answers);
		List<QuestionAndAnswer> GetQuiz();
		bool? CheckAnswer(long id);
		void UpRank(Rank officer);

		void SetPoliceShift(Shift shift);
		IEnumerable<BasePolicemanShiftVM> GetShifts();
		IEnumerable<SheriffShiftVM> GetOfficerShift();
		Shift UpdateShift(UpdateShiftViewModel shift);
	}
}
