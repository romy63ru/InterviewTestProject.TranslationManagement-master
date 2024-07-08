import { useForm } from "react-hook-form";
import { Page } from "./Page";
import {
  FieldContainer,
  FieldInput,
  FieldLabel,
  Fieldset,
  FieldTextArea,
  FormButtonContainer,
  PrimaryButton,
  SubmissionSuccess,
} from "./Styles";
import { TranslationJob, TranslationJobImpl } from "./TranslationJob";
import { createJob } from "./Data";
import React from "react";

export const NewJobPage = () => {
  const [successfullySubmitted, setSuccessfullySubmitted] =
    React.useState(false);

  const { handleSubmit, formState } = useForm<TranslationJobImpl>({
    mode: "onBlur",
  });

  const submitForm = async (data: TranslationJob) => {
    const result = await createJob(data);
    setSuccessfullySubmitted(result ? true : false);
  };

  return (
    <Page title="Add new job">
      <form onSubmit={handleSubmit(submitForm)}>
        <Fieldset disabled={formState.isSubmitting || successfullySubmitted}>
          <FieldContainer>
            <FieldLabel htmlFor="title">Customer name</FieldLabel>
            <FieldInput id="customerName" name="customerName" type="text" />
          </FieldContainer>
          <FieldContainer>
            <FieldLabel htmlFor="content">Original Content</FieldLabel>
            <FieldTextArea id="originalContent" name="originalContent" />
          </FieldContainer>
          <FieldContainer>
            <FieldLabel htmlFor="content">Translated Content</FieldLabel>
            <FieldTextArea id="translatedContent" name="translatedContent" />
          </FieldContainer>
          <FieldContainer>
            <FieldLabel htmlFor="title">Price</FieldLabel>
            <FieldInput id="price" name="price" type="text" />
          </FieldContainer>
          <FormButtonContainer>
            <PrimaryButton type="submit">Submit Your Job</PrimaryButton>
          </FormButtonContainer>
          {successfullySubmitted && (
            <SubmissionSuccess>
              Your job was successfully submitted
            </SubmissionSuccess>
          )}
        </Fieldset>
      </form>
    </Page>
  );
};

export default NewJobPage;
