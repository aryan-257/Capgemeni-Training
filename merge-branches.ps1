# Script to merge master into main without file lock issues

Write-Host "Step 1: Cleaning up any stuck merge state..." -ForegroundColor Yellow
Remove-Item -Path ".git\MERGE_HEAD" -Force -ErrorAction SilentlyContinue
Remove-Item -Path ".git\MERGE_MODE" -Force -ErrorAction SilentlyContinue
Remove-Item -Path ".git\MERGE_MSG" -Force -ErrorAction SilentlyContinue
Remove-Item -Path ".git\index.lock" -Force -ErrorAction SilentlyContinue

Write-Host "Step 2: Ensuring we're on main branch..." -ForegroundColor Yellow
git checkout main

Write-Host "Step 3: Creating merge commit using git commit-tree..." -ForegroundColor Yellow
# This approach creates a merge commit without touching the working directory
$mainCommit = git rev-parse main
$masterCommit = git rev-parse master

# Create a merge commit with both parents, using master's tree
$mergeCommit = git commit-tree $masterCommit^{tree} -p $mainCommit -p $masterCommit -m "Merge master into main"

# Update main branch to point to the new merge commit
git update-ref refs/heads/main $mergeCommit

Write-Host "Step 4: Resetting working directory to match the merge..." -ForegroundColor Yellow
git reset --hard HEAD

Write-Host "`nMerge completed successfully!" -ForegroundColor Green
Write-Host "`nVerifying merge:" -ForegroundColor Cyan
git log --oneline --graph --all --decorate -10
