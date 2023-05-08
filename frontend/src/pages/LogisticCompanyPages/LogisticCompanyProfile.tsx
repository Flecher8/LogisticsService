import { useState, useEffect, FC } from "react";
import { Button, Card, Modal } from "react-bootstrap";
import { LogisticCompanyPanel } from "../../components/LogisticCompanyPanel";
import { LogisticCompaniesService, LogisticCompany } from "../../api/services/LogisticCompaniesService";
import { Rate, RatesService } from "../../api/services/RatesService";
import { UpdateRate } from "../../components/UpdateRate";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const LogisticCompanyProfile: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const [profile, setProfile] = useState<LogisticCompany | null>(null);
	const [rate, setRate] = useState<Rate>();

	// Update modal show
	const [updateItemModelShow, SetUpdateItemModelShow] = useState(false);
	const updateItemModelHandleClose = () => SetUpdateItemModelShow(false);
	const updateItemModelHandleShow = () => SetUpdateItemModelShow(true);

	function handleEditItem() {
		updateItemModelHandleShow();
	}

	const getProfileData = async (): Promise<void> => {
		try {
			const userId: number = parseInt(localStorage["userId"]);
			const logisticCompany: LogisticCompany | null = await LogisticCompaniesService.prototype.getItem(userId);
			setProfile(logisticCompany);

			const rate: Rate | null = await RatesService.prototype.getItemByLogisticCompany(userId);

			if (rate === null) return;

			setRate(rate);
		} catch (err) {
			alert(err);
		}
	};

	useEffect(() => {
		getProfileData();
	}, []);
	return (
		<div className="LogisticCompanyProfile container">
			<div className="d-flex border border-dark w-100">
				<LogisticCompanyPanel />
			</div>
			<Modal size="lg" centered show={updateItemModelShow} onHide={updateItemModelHandleClose}>
				<UpdateRate close={updateItemModelHandleClose} item={rate} />
			</Modal>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>{t("Profile")}</h1>
					</div>
				</header>
				<div>
					{profile !== null ? (
						<Card className="my-5">
							<Card.Header>{t("Profile Information")}</Card.Header>
							<Card.Body>
								<Card.Text>
									{t("Company name")}: {`${profile.companyName}`}
								</Card.Text>
								<Card.Text>
									{t("Email")}: {`${profile.email}`}
								</Card.Text>
								<Card.Text>
									{t("Description")}: {`${profile.description === null ? "" : profile.description}`}
								</Card.Text>
								<Card.Text>
									{t("Rate")}: {`${rate?.priceForKmInDollar}`} $ {t("per km")}
								</Card.Text>
							</Card.Body>
						</Card>
					) : (
						<p>{t("No data")}</p>
					)}
				</div>
				<div>
					<Button onClick={() => handleEditItem()} variant="outline-dark">
						{t("Update rate")}
					</Button>
				</div>
			</div>
		</div>
	);
};
