/** @jsxImportSource @emotion/react */
import { css } from "@emotion/react";
import styled from "@emotion/styled";
import { TranslationJob } from "./TranslationJob";
import { gray2 } from "./Styles";

interface Props {
  data: TranslationJob;
}

const Container = styled.div((props) => ({
  display: "flex",
  flexDirection: "row",
}));

export function Job({ data }: Props) {
  return (
    <div
      css={css`
        padding: 10px 0px;
      `}
    >
      <Container>
        <div
          css={css`
            padding: 10px 0px;
            font-size: 19px;
          `}
        >
          <div
            css={css`
              padding-bottom: 10px;
              font-size: 15px;
              color: ${gray2};
            `}
          >
            Id: {data.id}
          </div>
          <div
            css={css`
              padding-bottom: 10px;
              font-size: 15px;
              color: ${gray2};
            `}
          >
            Customer name: {data.customerName}
          </div>
          <div
            css={css`
              padding-bottom: 10px;
              font-size: 15px;
              color: ${gray2};
            `}
          >
            Original content: {data.originalContent}
          </div>
          <div
            css={css`
              padding-bottom: 10px;
              font-size: 15px;
              color: ${gray2};
            `}
          >
            Translated content: {data.translatedContent}
          </div>
          <div
            css={css`
              padding-bottom: 10px;
              font-size: 15px;
              color: ${gray2};
            `}
          >
            Price:
            {data.price}
          </div>
        </div>
      </Container>
    </div>
  );
}
