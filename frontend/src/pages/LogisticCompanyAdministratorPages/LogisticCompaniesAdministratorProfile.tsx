import { useState, useEffect, FC } from "react";
import { Button, Card } from "react-bootstrap";
import {
	LogisticCompaniesAdministrator,
	LogisticCompaniesAdministratorsService
} from "../../api/services/LogisticCompaniesAdministratorsService";
import { LogisticCompaniesAdministratorPanel } from "../../components/LogisticCompaniesAdministratorPanel";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const LogisticCompaniesAdministratorProfile: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const [profile, setProfile] = useState<LogisticCompaniesAdministrator | null>(null);

	const getProfileData = async (): Promise<void> => {
		try {
			const userId: number = parseInt(localStorage["userId"]);
			const response: LogisticCompaniesAdministrator | null =
				await LogisticCompaniesAdministratorsService.prototype.getItem(userId);
			setProfile(response);
		} catch (err) {
			alert(err);
		}
	};

	useEffect(() => {
		getProfileData();
	}, []);
	return (
		<div className="LogisticCompanyAdministratorProfile container">
			<div className="d-flex border border-dark w-100">
				<LogisticCompaniesAdministratorPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>{t("Profile")}</h1>
					</div>
				</header>
				{profile && (
					<Card className="my-5">
						<Card.Header>{t("Profile Information")}</Card.Header>
						<Card.Body>
							<Card.Text>
								{t("Name")}: {`${profile.firstName} ${profile.lastName}`}
							</Card.Text>
							<Card.Text>
								{t("Email")}: {`${profile.email}`}
							</Card.Text>
						</Card.Body>
					</Card>
				)}
			</div>
		</div>
	);
};
