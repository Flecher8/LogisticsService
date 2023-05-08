import { useState, useEffect, FC } from "react";
import { Link } from "react-router-dom";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { PaymentService } from "../api/services/PaymentService";
import { useTranslationHelper } from "../helpers/translation/translationService";

interface PaymentOrderProps {
	orderId: number;
	price: number;
	close(): void;
}

interface PaymentData {
	cardNumber: string;
	expirationDate: string;
	cvv: string;
	nameOnCard: string;
}

export const PaymentOrder: FC<PaymentOrderProps> = ({ orderId, price, close }) => {
	const { t, changeLanguage } = useTranslationHelper();

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

	async function handle() {
		try {
			await PaymentService.prototype.payForOrder(orderId);
			close();
			window.location.reload();
		} catch (err) {
			alert(err);
		}
	}
	useEffect(() => {
		if (orderId === 0) {
			close();
		}
	}, []);

	return (
		<div className="CreateSubscriptionType">
			<div className="container">
				<header>
					<div className="text-center mt-5">
						<h1>{t("Payment")}</h1>
					</div>
					<div className="text-center mt-5">
						<h2>
							{t("Price")}: {price} $
						</h2>
					</div>
				</header>
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
	);
};
