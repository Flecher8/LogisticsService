import { useState, useEffect, FC } from "react";
import { Link } from "react-router-dom";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { SubscriptionType, SubscriptionTypesService } from "../../api/services/SubscriptionTypesService";
import { UpdateModelProps } from "../../helpers/interfaces/UpdateModelProps";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const UpdateSubscriptionType: FC<UpdateModelProps<SubscriptionType>> = (
	props: UpdateModelProps<SubscriptionType>
) => {
	const { t, changeLanguage } = useTranslationHelper();

	const [subscriptionTypeId, setSubscriptionTypeId] = useState<number | undefined>(0);
	const [subscriptionTypeName, setSubscriptionTypeName] = useState<string>("");
	const [durationInDays, setDurationInDays] = useState<number>(0);
	const [price, setPrice] = useState<number>(0);

	async function handle() {
		try {
			let subscriptionType: SubscriptionType = {
				subscriptionTypeId: subscriptionTypeId,
				subscriptionTypeName: subscriptionTypeName,
				durationInDays: durationInDays,
				price: price
			};
			await SubscriptionTypesService.prototype.update(subscriptionType);
			props.close();
			window.location.reload();
		} catch (err) {
			alert(err);
		}
	}

	// onLoad function
	useEffect(() => {
		if (props.item !== undefined) {
			setSubscriptionTypeId(props.item.subscriptionTypeId);
			setSubscriptionTypeName(props.item.subscriptionTypeName);
			setDurationInDays(props.item.durationInDays);
			setPrice(props.item.price);
		}
	}, []);

	return (
		<div className="UpdateSubscriptionType">
			<div className="container">
				<header>
					<div className="text-center mt-5">
						<h1>{t("Update")}</h1>
					</div>
				</header>
				<div>
					<div className="mt-5 ml-5 mr-5">
						<Form>
							<Form.Group className="mb-3">
								<Form.Label>{t("Subscription Type Name")}</Form.Label>
								<Form.Control
									type="text"
									placeholder=""
									value={subscriptionTypeName}
									onChange={e => setSubscriptionTypeName(e.target.value)}
								/>
							</Form.Group>

							<Form.Group className="mb-3">
								<Form.Label>{t("Duration in days")}</Form.Label>
								<Form.Control
									type="number"
									placeholder="30"
									value={durationInDays}
									onChange={e => setDurationInDays(parseInt(e.target.value))}
								/>
							</Form.Group>

							<Form.Group className="mb-3">
								<Form.Label>{t("Price")}</Form.Label>
								<InputGroup>
									<FormControl
										type="number"
										placeholder="100"
										value={price}
										onChange={e => setPrice(parseInt(e.target.value))}
									/>
								</InputGroup>
							</Form.Group>
						</Form>
						<div>
							<Button className="mb-5" onClick={() => handle()}>
								{t("Update")}
							</Button>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};
