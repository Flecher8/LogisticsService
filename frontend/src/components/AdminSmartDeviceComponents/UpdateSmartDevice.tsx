import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Form } from "react-bootstrap";
import { UpdateModelProps } from "../../helpers/interfaces/UpdateModelProps";
import { SmartDevice, SmartDevicesService } from "../../api/services/SmartDevicesService";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const UpdateSmartDevice: FC<UpdateModelProps<SmartDevice>> = (props: UpdateModelProps<SmartDevice>) => {
	const { t, changeLanguage } = useTranslationHelper();

	const [itemId, setItemId] = useState<number | undefined>(0);
	const [logisticCompanyId, setLogisticCompanyId] = useState<number>(0);
	const [numberOfSensors, setNumberOfSensors] = useState<number>(0);

	async function handle() {
		try {
			let item: SmartDevice = {
				smartDeviceId: itemId,
				logisticCompanyId: logisticCompanyId,
				numberOfSensors: numberOfSensors
			};
			await SmartDevicesService.prototype.update(item);
			props.close();
			window.location.reload();
		} catch (err) {
			alert(err);
		}
	}

	// onLoad function
	useEffect(() => {
		if (props.item !== undefined) {
			setItemId(props.item.smartDeviceId);
			setLogisticCompanyId(props.item.logisticCompanyId);
			setNumberOfSensors(props.item.numberOfSensors);
		}
	}, []);

	return (
		<div className="UpdateSmartDevice">
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
								<Form.Label>{t("Logistic Company Id")}</Form.Label>
								<InputGroup>
									<FormControl
										type="number"
										placeholder="123"
										value={logisticCompanyId}
										onChange={e => setLogisticCompanyId(parseInt(e.target.value))}
									/>
								</InputGroup>
							</Form.Group>
							<Form.Group className="mb-3">
								<Form.Label>{t("Number of Sensors")}</Form.Label>
								<InputGroup>
									<FormControl
										type="number"
										placeholder="0"
										value={numberOfSensors}
										onChange={e => setNumberOfSensors(parseInt(e.target.value))}
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
