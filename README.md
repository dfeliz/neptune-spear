## Introduction :tada:

Hello there! This is a simple MVC project that uses an API to communicate with the database. 

## Start coding :rocket: (ignore this if you know how to use git)

To contribute, you must know github basics:

### Installing git and cloning the repo
1. Install git in your pc (duh 😂) here is the link: https://git-scm.com/downloads
2. Clone the project. You can achieve this by moving to the folder you want the project to be, and typing the following:
```bash
git clone https://username:password@github.com/dfeliz/neptune-spear.git
```
* username and password are your github credentials.

And that's it! Now you have the project in your machine.

### Start coding

To start coding, first **you MUST create a branch.** I won't explain what a branch is, I didn't get enough time. Google it, for the sake of your knowledge.

1. Go to your project folder.
2. Right click on an empty space and you'll see something like "Open git bash here". Click it.
3. Run the following:
  ```bash
  git checkout -b "my-name"
  ``` 
* Replace "my-name" with your name.

4. Open visual studio and do your job.

Probably you won't have the database, you MUST create it using the models and change the connection string of the project. GOOGLE IT!
If you don't do it, you will not be able to run the program.

### Uploading your code!

To upload your changes, do the following:
```bash
git add *
git commit -m "What did you do"
git push --set-upstream origin "my-name"
```
* "What did you do" is the commit message. Replace that with... eh... what did you do lol.
* "my-name" is your branch's name. Remember git checkout -b "my-name"? Well, that name.

And that's all! Now all you have to do is come to this page **AGAIN** and you will see at the top a yellow container that says "You commited blabla changes" and a green button that says **"Compare & create pull request"**. Click that green button, then scroll down and press the another green button that says **"Create pull request"**.

**That's it!** Now you know how to use git. Well, a 1% at least.
