import { useState, useEffect, FC } from "react";
import { Button, Card, Form } from "react-bootstrap";

import { LogisticCompanyPanel } from "../../components/LogisticCompanyPanel";
import { SubscriptionType } from "../../api/services/SubscriptionTypesService";

import { useTranslationHelper } from "../../helpers/translation/translationService";
import { SubscriptionTypesService } from "../../api/services/SubscriptionTypesService";
import { SubscriptionTypesList } from "../../components/SubscriptionTypesList";
import { PaymentData } from "../../helpers/interfaces/PaymentData";
import { SubscriptionDto, SubscriptionsService } from "../../api/services/SubscriptionsService";
import { SubscriptionBuyPanel } from "../../components/SubscriptionBuyPanel";

export const LogisticCompanyBuySubscription: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const [subscriptionTypes, setSubscriptionTypes] = useState<SubscriptionType[] | null>(null);
	const [selectedSubscriptionTypeId, setSelectedSubscriptionTypeId] = useState<number>(0);

	const [paymentData, setPaymentData] = useState<PaymentData>({
		cardNumber: "",
		expirationDate: "",
		cvv: "",
		nameOnCard: ""
	});

	const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
		const { name, value } = event.target;
		setPaymentData({ ...paymentData, [name]: value });
	};

	const getSubscriptionTypes = async (): Promise<void> => {
		try {
			const response: SubscriptionType[] | null = await SubscriptionTypesService.prototype.getAll();
			if (response === null) return;

			setSubscriptionTypes(response);
		} catch (err) {}
	};

	async function handle() {
		if (selectedSubscriptionTypeId === 0) {
			const subscriptionWasNotSelectedText = t("Subscription was not selected") ?? "";
			alert(subscriptionWasNotSelectedText);
			return;
		}
		try {
			let subsciptionDto: SubscriptionDto = {
				logisticCompanyId: parseInt(localStorage["userId"]),
				subscriptionTypeId: selectedSubscriptionTypeId
			};
			await SubscriptionsService.prototype.put(subsciptionDto);
			window.location.href = "/LogisticCompanyProfile";
		} catch (err) {
			alert(err);
		}
	}

	useEffect(() => {
		getSubscriptionTypes();
	}, []);

	return (
		<div className="LogisticCompanyBuySubscription container">
			<div className="d-flex border border-dark w-100">
				<SubscriptionBuyPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>{t("You need to buy subscription in order to use this service")}</h1>
					</div>
				</header>
				<div>
					<div className="text-center mt-5">
						<h2>{t("Payment")}</h2>
					</div>
					<div className="mt-5 ml-4">
						<h3>{t("Select subscription type")}</h3>
					</div>
					<div>
						{subscriptionTypes !== null ? (
							<SubscriptionTypesList
								subscriptionTypes={subscriptionTypes === null ? [] : subscriptionTypes}
								selectedItemId={selectedSubscriptionTypeId}
								onSelectItem={setSelectedSubscriptionTypeId}
							/>
						) : (
							<p>{t("No data")}</p>
						)}
					</div>
				</div>
				<div className="container">
					<div>
						<div className="mt-5 ml-5 mr-5">
							<Form>
								<Form.Group controlId="cardNumber">
									<Form.Label>{t("Card Number")}</Form.Label>
									<Form.Control
										type="text"
										name="cardNumber"
										value={paymentData.cardNumber}
										onChange={handleChange}
										placeholder="1234567890"
									/>
								</Form.Group>
								<Form.Group controlId="expirationDate">
									<Form.Label>{t("Expiration Date")}</Form.Label>
									<Form.Control
										type="text"
										name="expirationDate"
										value={paymentData.expirationDate}
										onChange={handleChange}
										placeholder="MM/YY"
									/>
								</Form.Group>
								<Form.Group controlId="cvv">
									<Form.Label>CVV</Form.Label>
									<Form.Control
										type="text"
										name="cvv"
										value={paymentData.cvv}
										onChange={handleChange}
										placeholder="123"
									/>
								</Form.Group>
								<Form.Group controlId="nameOnCard">
									<Form.Label>{t("Name on Card")}</Form.Label>
									<Form.Control
										type="text"
										name="nameOnCard"
										value={paymentData.nameOnCard}
										onChange={handleChange}
										placeholder="Tom Care"
									/>
								</Form.Group>
							</Form>
							<Button className="mt-3 mb-3" variant="primary" onClick={() => handle()}>
								{t("Submit")}
							</Button>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};
