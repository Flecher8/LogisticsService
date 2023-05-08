import { useState, useEffect, FC } from "react";
import { Button, Card } from "react-bootstrap";
import { Link } from "react-router-dom";
import { AdminPanel } from "../../components/AdminPanel";
import { SystemAdmin, SystemAdminService } from "../../api/services/SystemAdminService";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const SystemAdminProfile: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const [profile, setProfile] = useState<SystemAdmin | null>(null);

	const getProfileData = async (): Promise<void> => {
		try {
			const userId: number = parseInt(localStorage["userId"]);
			const response: SystemAdmin | null = await SystemAdminService.prototype.getItem(userId);
			setProfile(response);
		} catch (err) {
			alert(err);
		}
	};

	useEffect(() => {
		getProfileData();
	}, []);
	return (
		<div className="SystemAdminProfile container">
			<div className="d-flex border border-dark w-100">
				<AdminPanel />
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
