import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { UpdateModelProps } from "../helpers/interfaces/UpdateModelProps";
import { Rate, RatesService } from "../api/services/RatesService";
import { useTranslationHelper } from "../helpers/translation/translationService";

export const UpdateRate: FC<UpdateModelProps<Rate>> = (props: UpdateModelProps<Rate>) => {
	const { t, changeLanguage } = useTranslationHelper();

	const [priceForKmInDollar, setPriceForKmInDollar] = useState<number>(0);

	async function handle() {
		try {
			if (props.item === undefined) return;
			if (!validated()) return;

			let rate: Rate = {
				rateId: props.item.rateId,
				priceForKmInDollar: priceForKmInDollar
			};
			await RatesService.prototype.update(rate);
			props.close();
			window.location.reload();
		} catch (err) {
			alert(err);
		}
	}

	function validated(): boolean {
		if (priceForKmInDollar > 0) {
			return true;
		}
		const priceMustBeGreaterThan0Text = t("Price must be greater than 0") ?? "";
		alert(priceMustBeGreaterThan0Text);
		return false;
	}

	// onLoad function
	useEffect(() => {
		if (props.item !== undefined) {
			setPriceForKmInDollar(props.item.priceForKmInDollar);
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
								<Form.Label>{t("Price for Km in Dollar")}</Form.Label>
								<Form.Control
									type="number"
									placeholder="0"
									value={priceForKmInDollar}
									onChange={e => setPriceForKmInDollar(parseInt(e.target.value))}
								/>
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
