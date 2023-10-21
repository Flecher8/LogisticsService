import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Form } from "react-bootstrap";
import { CreateModelProps } from "../../helpers/interfaces/ChangeModelProps";
import { SensorDto, SensorsService } from "../../api/services/SensorsService";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const CreateSensor: FC<CreateModelProps> = (props: CreateModelProps) => {
	const { t, changeLanguage } = useTranslationHelper();

	const [smartDeviceId, setSmartDeviceId] = useState<number>(0);

	async function handle() {
		try {
			let item: SensorDto = {
				smartDeviceId: smartDeviceId
			};
			await SensorsService.prototype.create(item);
			props.close();
			window.location.reload();
		} catch (err) {
			// TODO Languages
			// Error: Specified argument was out of the range of valid values. (Parameter 'SmartDevice with such id does not exist')
			alert(err);
		}
	}

	return (
		<div className="CreateSensor">
			<div className="container">
				<header>
					<div className="text-center mt-5">
						<h1>{t("Create")}</h1>
					</div>
				</header>
				<div>
					<div className="mt-5 ml-5 mr-5">
						<Form>
							<Form.Group className="mb-3">
								<Form.Label>{t("Smart device id")}</Form.Label>
								<InputGroup>
									<FormControl
										type="number"
										placeholder="123"
										value={smartDeviceId}
										onChange={e => setSmartDeviceId(parseInt(e.target.value))}
									/>
								</InputGroup>
							</Form.Group>
						</Form>
						<div>
							<Button className="mb-5" onClick={() => handle()}>
								{t("Create")}
							</Button>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};
